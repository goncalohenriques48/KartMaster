namespace KartMaster.Models {
    /// <summary>
    /// DTO (Data Transfer Object) utilizado para criação ou atualização de reservas.
    /// Contém os dados necessários para persistência e operações de negócio.
    /// </summary>
    public class ReservaDto {
        /// <summary>
        /// Nome do reservante.
        /// </summary>
        public string NomeReservante { get; set; } = string.Empty;
        /// <summary>
        /// Número total de pessoas incluídas na reserva.
        /// </summary>
        public int NumeroPessoas { get; set; }
        /// <summary>
        /// Data da reserva (ex: 2025-07-10).
        /// </summary>
        public DateTime Data { get; set; }
        /// <summary>
        /// Hora da reserva (ex: 14:30:00).
        /// </summary>
        public TimeSpan Hora { get; set; }
        /// <summary>
        /// Duração da reserva (ex: 01:30:00).
        /// </summary>
        public TimeSpan Duracao { get; set; }
        /// <summary>
        /// ID do autódromo associado à reserva.
        /// </summary>
        public int AutodromoId { get; set; }
        /// <summary>
        /// ID do utilizador que efetuou a reserva (pode ser nulo para reservas anónimas ou temporárias).
        /// </summary>
        public string? UtilizadorId { get; set; }
        /// <summary>
        /// ID da corrida associada à reserva.
        /// </summary>
        public int CorridaId { get; set; }
    }
}
