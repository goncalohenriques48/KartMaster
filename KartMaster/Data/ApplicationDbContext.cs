using KartMaster.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KartMaster.Data;
/// <summary>
/// Classe de contexto da base de dados para a aplicação KartMaster.
/// Herda de IdentityDbContext para incluir suporte a autenticação e gestão de utilizadores.
/// </summary>
public class ApplicationDbContext : IdentityDbContext
{
    /// <summary>
    /// Construtor que recebe opções de configuração do contexto.
    /// </summary>
    /// <param name="options">Opções de configuração do contexto.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    /// <summary>
    /// Conjunto de dados que representa os autódromos.
    /// </summary>
    public DbSet<Autodromo> Autodromos { get; set; }

    /// <summary>
    /// Conjunto de dados que representa as corridas.
    /// </summary>
    public DbSet<Corrida> Corridas { get; set; }

    /// <summary>
    /// Conjunto de dados que representa os utilizadores.
    /// </summary>
    public DbSet<Utilizador> Utilizadores { get; set; }

    /// <summary>
    /// Conjunto de dados que representa as participações.
    /// </summary>
    public DbSet<Participacao> Participacoes { get; set; }

    /// <summary>
    /// Conjunto de dados que representa as reservas efetuadas por utilizadores.
    /// </summary>
    public DbSet<Reserva> Reservas { get; set; }


    /// <summary>
    /// Configurações adicionais do modelo de dados, incluindo relacionamentos e restrições.
    /// </summary>
    /// <param name="builder">Construtor de modelos do Entity Framework.</param>
    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        // Define a relação 1:1 entre Utilizador e IdentityUser
        builder.Entity<Utilizador>()
            .HasOne(u => u.IdentityUser)
            .WithOne()
            .HasForeignKey<Utilizador>(u => u.IdentityUserId);

        // Define a relação entre Reserva e Corrida, com restrição de eliminação
        builder.Entity<Reserva>()
            .HasOne(r => r.Corrida)
            .WithMany()
            .HasForeignKey(r => r.CorridaId)
            .OnDelete(DeleteBehavior.Restrict); // evita múltiplos caminhos de cascade delete
    }


}
