namespace KartMaster.Models {
    /// <summary>
    /// ViewModel para representar os dados de uma participação numa corrida.
    /// </summary>
    public class ParticipacaoViewModel {
        /// <summary>
        /// Nome da corrida em que o utilizador participou.
        /// </summary>
        public string? NomeCorrida { get; set; }
        /// <summary>
        /// Nome do utilizador que participou na corrida.
        /// </summary>
        public string? NomeUtilizador { get; set; }
        /// <summary>
        /// Posição final do utilizador na corrida (ex: "1º", "2º", etc.).
        /// </summary>
        public string? PosicaoFinal { get; set; }
        /// <summary>
        /// Tempo final registado na corrida (ex: "00:03:45").
        /// </summary>
        public string? TempoFinal { get; set; }
    }
}
