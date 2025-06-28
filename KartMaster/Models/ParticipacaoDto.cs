namespace KartMaster.Models {
    /// <summary>
    /// DTO (Data Transfer Object) usado para criar ou atualizar uma participação numa corrida.
    /// </summary>
    public class ParticipacaoDto {
        /// <summary>
        /// Identificador da corrida associada à participação.
        /// </summary>
        public int CorridaId { get; set; }
        /// <summary>
        /// Identificador do utilizador que participou na corrida.
        /// </summary>
        public int UtilizadorId { get; set; }
        /// <summary>
        /// Posição final alcançada pelo utilizador na corrida.
        /// </summary>
        public string PosicaoFinal { get; set; } = string.Empty;
        /// <summary>
        /// Tempo final registado pelo utilizador na corrida.
        /// </summary>
        public TimeSpan TempoFinal { get; set; }
    }
}
