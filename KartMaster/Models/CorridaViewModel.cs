namespace KartMaster.Models {
    public class CorridaViewModel {
        public string? Nome { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public TimeSpan Duracao { get; set; }
        public string? NomeAutodromo { get; set; }
        public int NumeroParticipantes { get; set; }
    }
}
