using System.ComponentModel.DataAnnotations;

namespace KartMaster.Models
{
    /// <summary>
    /// Representa um utilizador.
    /// </summary>
    public class Utilizador
    {
        /// <summary>
        /// Identificador �nico do utilizador.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome do utilizador.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Email do utilizador.
        /// </summary>
        public string Email { get; set; } = string.Empty;




        /* *************************
        * Defin��o dos relacionamentos
        * ************************** 
        */

        // Relacionamentos 1-N

        /// <summary>
        /// Lista das participa��es do utilizador, em corridas.
        /// </summary>
        public ICollection<Participacao> Participacoes { get; set; } = [];
    }
}

