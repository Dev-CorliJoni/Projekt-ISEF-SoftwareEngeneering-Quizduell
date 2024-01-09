namespace Quixduell.ServiceLayer.Services.MailSender.SMTP
{
    public class SMTPEmailConfiguration
    {
        public static string Section = "SMTPEmailConfiguration";
        public string SmtpServer { get; set; } = string.Empty;
        public int SmtpPort { get; set; } = 1025;
        public bool UseSSL { get; set; } = false;
        public string DefaultSenderMail { get; set; } = string.Empty;


        public bool CheckValues()
        {
            return !(string.IsNullOrEmpty(DefaultSenderMail) && string.IsNullOrEmpty(SmtpServer));
        }
    }
}
