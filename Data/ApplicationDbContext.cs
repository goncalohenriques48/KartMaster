using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KartMaster.Models
{
    /// <summary>
    /// Representa o contexto do banco de dados da aplicação.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <param name="options">As opções para configurar o contexto.</param>
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
    }
}

