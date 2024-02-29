namespace Quixduell.ServiceLayer.Services.MailSender.SMTP
{
    /// <summary>
    /// Represents the configuration for SMTP email sending.
    /// </summary>
    public class SMTPEmailConfiguration
    {
        /// <summary>
        /// The configuration section name.
        /// </summary>
        public static string Section = "SMTPEmailConfiguration";

        /// <summary>
        /// The SMTP server address.
        /// </summary>
        public string SmtpServer { get; set; } = string.Empty;

        /// <summary>
        /// The SMTP port.
        /// </summary>
        public int SmtpPort { get; set; } = 1025;

        /// <summary>
        /// Indicates whether to use SSL for SMTP connection.
        /// </summary>
        public bool UseSSL { get; set; } = false;

        /// <summary>
        /// The default sender email address.
        /// </summary>
        public string DefaultSenderMail { get; set; } = string.Empty;

        /// <summary>
        /// Checks if the configuration values are valid.
        /// </summary>
        /// <returns>True if the configuration is valid; otherwise, false.</returns>
        public bool CheckValues()
        {
            return !(string.IsNullOrEmpty(DefaultSenderMail) && string.IsNullOrEmpty(SmtpServer));
        }
    }
}
