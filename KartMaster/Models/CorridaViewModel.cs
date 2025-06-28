namespace KartMaster.Models {
    /// <summary>
    /// ViewModel que representa os dados de uma corrida para exibição em views.
    /// </summary>
    public class CorridaViewModel {
        /// <summary>
        /// Nome da corrida.
        /// </summary>
        public string? Nome { get; set; }
        /// <summary>
        /// Data da corrida.
        /// </summary>
        public DateTime Data { get; set; }
        /// <summary>
        /// Hora de início da corrida.
        /// </summary>
        public TimeSpan Hora { get; set; }
        /// <summary>
        /// Duração total da corrida.
        /// </summary>
        public TimeSpan Duracao { get; set; }
        /// <summary>
        /// Nome do autódromo onde a corrida ocorre.
        /// </summary>
        public string? NomeAutodromo { get; set; }
        /// <summary>
        /// Número total de participantes na corrida.
        /// </summary>
        public int NumeroParticipantes { get; set; }
    }
}
