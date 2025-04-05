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
        public string Nome { get; set; }

        /// <summary>
        /// Email do utilizador.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha do utilizador.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Tipo de utilizador (Anonimo, Piloto, Admin).
        /// </summary>
        public string Tipo { get; set; }

        /* *************************
        * Defin��o dos relacionamentos
        * ************************** 
        */

        // Relacionamentos 1-N

        /// <summary>
        /// Lista das participa��es do utilizador.
        /// </summary>
        public ICollection<Participacao> Participacoes { get; set; }
    }
}

