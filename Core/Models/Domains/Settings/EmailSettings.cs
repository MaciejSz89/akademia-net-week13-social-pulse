namespace SocialPulse.Core.Models.Domains.Settings
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = null!;
        public int SmtpPort { get; set; }
        public string FromEmail { get; set; } = null!;
        public string FromPassword { get; set; } = null!;

        public bool IsEmailConfigured =>
            !string.IsNullOrWhiteSpace(SmtpServer) &&
            SmtpPort > 0 &&
            !string.IsNullOrWhiteSpace(FromEmail) &&
            !string.IsNullOrWhiteSpace(FromPassword);
    }
}
