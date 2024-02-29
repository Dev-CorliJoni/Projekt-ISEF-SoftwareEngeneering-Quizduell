namespace Quixduell.ServiceLayer.Services.MailSender.SendGrid
{
    /// <summary>
    /// Represents the configuration for SendGrid email sending.
    /// </summary>
    public class SendGridEmailConfiguration
    {
        /// <summary>
        /// The configuration section name.
        /// </summary>
        public static string Section = "SendGridEmailConfiguration";

        /// <summary>
        /// The API key for SendGrid.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// The default sender email address.
        /// </summary>
        public string DefaultSender { get; set; } = string.Empty;

        /// <summary>
        /// Checks if the configuration values are valid.
        /// </summary>
        /// <returns>True if the configuration is valid; otherwise, false.</returns>
        public bool CheckValues()
        {
            return !(string.IsNullOrEmpty(ApiKey) && string.IsNullOrEmpty(DefaultSender));
        }
    }
}
