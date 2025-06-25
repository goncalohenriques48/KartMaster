using KartMaster.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KartMaster.Data;

public class ApplicationDbContext : IdentityDbContext
{
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

    public DbSet<Reserva> Reservas { get; set; }


    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        // Configurar o relacionamento entre Utilizador e IdentityUser
        builder.Entity<Utilizador>()
            .HasOne(u => u.IdentityUser)
            .WithOne()
            .HasForeignKey<Utilizador>(u => u.IdentityUserId);

        builder.Entity<Reserva>()
            .HasOne(r => r.Corrida)
            .WithMany()
            .HasForeignKey(r => r.CorridaId)
            .OnDelete(DeleteBehavior.Restrict); // evita múltiplos caminhos de cascade delete
    }


}
