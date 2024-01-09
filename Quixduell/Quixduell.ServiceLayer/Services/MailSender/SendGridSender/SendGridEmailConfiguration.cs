namespace Quixduell.ServiceLayer.Services.MailSender.SendGrid
{
    public class SendGridEmailConfiguration
    {
        public static string Section = "SendGridEmailConfiguration";
        public string ApiKey { get; set; } = string.Empty;
        public string DefaultSender { get; set; } = string.Empty;


        public bool CheckValues()
        {
            return !(string.IsNullOrEmpty(ApiKey) && string.IsNullOrEmpty(DefaultSender));
        }
    }
}
