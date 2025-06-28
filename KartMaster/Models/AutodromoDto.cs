namespace KartMaster.Models {
    /// <summary>
    /// Data Transfer Object (DTO) utilizado para transferir dados de um autódromo entre camadas da aplicação.
    /// </summary>
    public class AutodromoDto {
        /// <summary>
        /// Nome do autódromo.
        /// </summary>
        public string Nome { get; set; } = string.Empty;
        /// <summary>
        /// Localização geográfica do autódromo.
        /// </summary>
        public string Localizacao { get; set; } = string.Empty;
        /// <summary>
        /// Número de telemóvel de contacto do autódromo.
        /// </summary>
        public string Telemovel { get; set; } = string.Empty;
        /// <summary>
        /// Endereço de email de contacto do autódromo.
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Capacidade máxima de pessoas permitidas no autódromo.
        /// </summary>
        public int Capacidade { get; set; }
    }
}
