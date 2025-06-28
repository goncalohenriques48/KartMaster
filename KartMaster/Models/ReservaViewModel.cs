namespace KartMaster.Models {
    /// <summary>
    /// ViewModel utilizado para representar os dados essenciais de uma reserva,
    /// incluindo informações do reservante, horário e entidades relacionadas.
    /// </summary>
    public class ReservaViewModel {
        /// <summary>
        /// Nome da pessoa que fez a reserva.
        /// </summary>
        public string? NomeReservante { get; set; }
        /// <summary>
        /// Número de pessoas incluídas na reserva.
        /// </summary>
        public int NumeroPessoas { get; set; }
        /// <summary>
        /// Data da reserva no formato "dd-MM-yyyy".
        /// </summary>
        public string? Data { get; set; }
        /// <summary>
        /// Hora da reserva no formato "HH:mm:ss".
        /// </summary>
        public string? Hora { get; set; }
        /// <summary>
        /// Duração da reserva no formato "HH:mm:ss".
        /// </summary>
        public string? Duracao { get; set; }
        /// <summary>
        /// Nome do autódromo onde a reserva foi feita.
        /// </summary>
        public string? NomeAutodromo { get; set; }
        /// <summary>
        /// Nome da corrida associada à reserva, se aplicável.
        /// </summary>
        public string? NomeCorrida { get; set; }
    }
}
