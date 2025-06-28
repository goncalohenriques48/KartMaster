namespace KartMaster.Models {
    /// <summary>
    /// DTO utilizado para transferência de dados de criação ou edição de uma corrida.
    /// </summary>
    public class CorridaDto {
        /// <summary>
        /// Nome da corrida.
        /// </summary>
        public string Nome { get; set; } = string.Empty;
        /// <summary>
        /// Data em que a corrida ocorre.
        /// </summary>
        public DateTime Data { get; set; }
        /// <summary>
        /// Hora de início da corrida.
        /// </summary>
        public TimeSpan Hora { get; set; }
        /// <summary>
        /// Duração da corrida.
        /// </summary>
        public TimeSpan Duracao { get; set; }
        /// <summary>
        /// ID do autódromo associado à corrida.
        /// </summary>
        public int AutodromoId { get; set; }
    }
}
