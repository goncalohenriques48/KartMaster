namespace KartMaster.Models {
    public class CorridaDto {
        public string Nome { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public TimeSpan Duracao { get; set; }
        public int AutodromoId { get; set; }
    }
}
