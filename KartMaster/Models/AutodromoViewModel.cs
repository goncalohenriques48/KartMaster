namespace KartMaster.Models {
    /// <summary>
    /// ViewModel utilizado para apresentar ou recolher dados de um autódromo.
    /// </summary>
    public class AutodromoViewModel {
        /// <summary>
        /// Nome do autódromo.
        /// </summary>
        public string? Nome { get; set; }
        /// <summary>
        /// Localização geográfica do autódromo.
        /// </summary>
        public string? Localizacao { get; set; }
        /// <summary>
        /// Número de telemóvel de contacto do autódromo.
        /// </summary>
        public string? Telemovel { get; set; }
        /// <summary>
        /// Endereço de email de contacto do autódromo.
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Capacidade máxima de pessoas permitidas no autódromo.
        /// </summary>
        public int Capacidade { get; set; }
    }
}
