namespace KartMaster.Services {
    public class AuthMessageSenderOptions {
        public required string SendGridApiKey { get; set; }
        public required string FromEmail { get; set; }
        public required string FromName { get; set; }
    }
}
