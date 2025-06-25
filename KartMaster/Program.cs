using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KartMaster.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using KartMaster.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddHttpClient(); // Necess�rio para IHttpClientFactory


builder.Services.AddTransient<IEmailSender, EmailSender>();

// Configurar AuthMessageSenderOptions
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection("AuthMessageSenderOptions"));

// Registrar o EmailSender
builder.Services.AddTransient<IEmailSender, EmailSender>();


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultUI()
.AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "KartMaster API v1");
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Criar a role "Admin" se ainda não existir
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Procurar o utilizador por email (ajusta se necessário)
    var user = await userManager.FindByEmailAsync("kartmaster0717@gmail.com");

    if (user != null && !await userManager.IsInRoleAsync(user, "Admin"))
    {
        await userManager.AddToRoleAsync(user, "Admin");
    }
}


app.Run();
