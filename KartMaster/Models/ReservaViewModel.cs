namespace KartMaster.Models {
    public class ReservaViewModel {
        public string? NomeReservante { get; set; }
        public int NumeroPessoas { get; set; }
        public string? Data { get; set; }           // Ex: "10-07-2025"
        public string? Hora { get; set; }           // Ex: "14:30:00"
        public string? Duracao { get; set; }        // Ex: "01:00:00"
        public string? NomeAutodromo { get; set; }
        public string? NomeCorrida { get; set; }
    }
}
