using KartMaster.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

public class EmailSender : IEmailSender {
    private readonly AuthMessageSenderOptions _options;

    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor) {
        _options = optionsAccessor.Value;
    }

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
