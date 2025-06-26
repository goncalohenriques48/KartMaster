namespace KartMaster.Models {
    public class ReservaDto {
        public string NomeReservante { get; set; } = string.Empty;
        public int NumeroPessoas { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public TimeSpan Duracao { get; set; }
        public int AutodromoId { get; set; }
        public string? UtilizadorId { get; set; }
        public int CorridaId { get; set; }
    }
}
