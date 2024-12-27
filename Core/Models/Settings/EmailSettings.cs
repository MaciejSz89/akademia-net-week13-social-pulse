namespace SocialPulse.Core.Models.Settings
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = null!;
        public int SmtpPort { get; set; } 
        public string FromEmail { get; set; } = null!;
        public string FromPassword { get; set; } = null!;
    }
}
