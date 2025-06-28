namespace KartMaster.Models {
    /// <summary>
    /// ViewModel utilizado para representar os dados básicos de um utilizador,
    /// normalmente em formulários ou exibições simplificadas.
    /// </summary>
    public class UtilizadorViewModel {
        /// <summary>
        /// Nome do utilizador.
        /// </summary>
        public string Nome { get; set; } = string.Empty;
        /// <summary>
        /// Endereço de email do utilizador.
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
