namespace KartMaster.Services {
    /// <summary>
    /// Representa as opções de configuração para o envio de emails através do SendGrid.
    /// Esta classe é utilizada para configurar a chave de API e os dados do remetente.
    /// </summary>
    public class AuthMessageSenderOptions {
        /// <summary>
        /// Chave da API do SendGrid utilizada para autenticar os pedidos de envio de email.
        /// </summary>
        public required string SendGridApiKey { get; set; }
        /// <summary>
        /// Endereço de email do remetente padrão (utilizado nos emails enviados pela aplicação).
        /// </summary>
        public required string FromEmail { get; set; }
        /// <summary>
        /// Nome do remetente a ser apresentado nos emails enviados.
        /// </summary>
        public required string FromName { get; set; }
    }
}
