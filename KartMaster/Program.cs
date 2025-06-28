// Imports essenciais
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KartMaster.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using KartMaster.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//
// CONFIGURAÇÃO DE SERVIÇOS
//

// Configuração da string de ligação à base de dados SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configura o contexto da base de dados com Entity Framework Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configuração de envio de emails com dependência injetada
builder.Services.AddHttpClient();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection("AuthMessageSenderOptions"));

// Configuração da autenticação Identity com roles e confirmação obrigatória de conta
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultUI()
.AddDefaultTokenProviders();

// Adiciona autenticação com JWT (JSON Web Tokens)
// JWT é usado apenas na API — o site usa cookies
builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            ),
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        };
    });

// Adiciona suporte para MVC (Controllers + Views) e Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//
// CONFIGURAÇÃO DO SWAGGER (documentação da API)
//
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo {
        Title = "KartMaster API",
        Version = "v1"
    });

    // Suporte para autenticação via JWT no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Description = "JWT Authorization header usando o esquema Bearer. Ex: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    // Define os requisitos de segurança para endpoints protegidos
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });

    // Inclui comentários XML no Swagger (necessário gerar XML no .csproj)
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "KartMaster.xml"));
});

//
// PIPELINE HTTP
//
var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint(); // Mostra página de migrações para devs
}
else {
    app.UseExceptionHandler("/Home/Error"); // Redireciona para página de erro em produção
    app.UseHsts(); // Ativa HTTP Strict Transport Security
}

app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
app.UseStaticFiles(); // Permite servir ficheiros estáticos (CSS, JS, imagens)

app.UseRouting(); // Ativa o sistema de rotas

// Ativa Swagger (UI e JSON endpoint)
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "KartMaster API v1");
});

// Ativa autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Define as rotas MVC padrão e ativa páginas Razor
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

//
// CRIAÇÃO DE ROLE ADMIN E ATRIBUIÇÃO A UTILIZADOR
//
using (var scope = app.Services.CreateScope()) {
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Cria o role "Admin" se ainda não existir
    if (!await roleManager.RoleExistsAsync("Admin")) {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Atribui o role "Admin" ao utilizador com o email especificado
    var user = await userManager.FindByEmailAsync("kartmaster0717@gmail.com");
    if (user != null && !await userManager.IsInRoleAsync(user, "Admin")) {
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Run(); // Inicia a aplicação
