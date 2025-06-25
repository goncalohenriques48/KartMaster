namespace KartMaster.Models {
    public class ParticipacaoDto {
        public int CorridaId { get; set; }
        public int UtilizadorId { get; set; }
        public string PosicaoFinal { get; set; } = string.Empty;
        public TimeSpan TempoFinal { get; set; }
    }
}
