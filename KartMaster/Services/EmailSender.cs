using KartMaster.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

/// <summary>
/// Serviço responsável pelo envio de emails utilizando a plataforma SendGrid.
/// Implementa a interface <see cref="IEmailSender"/> usada por Identity.
/// </summary>
public class EmailSender : IEmailSender {
    private readonly AuthMessageSenderOptions _options;

    /// <summary>
    /// Construtor da classe <see cref="EmailSender"/>.
    /// Recebe as opções de configuração necessárias para o envio de emails.
    /// </summary>
    /// <param name="optionsAccessor">Objeto que contém as opções configuradas para envio de emails.</param>
    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor) {
        _options = optionsAccessor.Value;
    }

    /// <summary>
    /// Envia um email assíncrono através do serviço SendGrid.
    /// </summary>
    /// <param name="email">Endereço de email do destinatário.</param>
    /// <param name="subject">Assunto do email.</param>
    /// <param name="htmlMessage">Conteúdo HTML do email.</param>
    /// <returns>Uma <see cref="Task"/> que representa a operação assíncrona.</returns>
    /// <exception cref="System.Exception">Lançada se a chave da API não estiver configurada ou ocorrer erro ao enviar.</exception>
    public async Task SendEmailAsync(string email, string subject, string htmlMessage) {
        if (string.IsNullOrEmpty(_options.SendGridApiKey)) {
            throw new System.Exception("SendGrid API Key is not configured.");
        }

        var client = new SendGridClient(_options.SendGridApiKey);
        var from = new EmailAddress(_options.FromEmail, _options.FromName);
        var to = new EmailAddress(email);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlMessage);
        var response = await client.SendEmailAsync(msg);

        if ((int)response.StatusCode >= 400) {
            throw new System.Exception($"Erro ao enviar email: {response.StatusCode}");
        }
    }
}
