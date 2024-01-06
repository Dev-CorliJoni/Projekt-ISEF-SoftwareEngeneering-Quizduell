namespace Quixduell.Blazor.Shared.AlertComponent
{
    public partial class AlertComponent
    {
        private List<AlertMessage> _alertMessages = new List<AlertMessage>();


        public void AddAlert(string messageText)
        {
            AddAlert(messageText, TimeSpan.FromSeconds(3), AlertMessageType.Success);
        }
        public void AddAlert(string messageText, TimeSpan displayTime, AlertMessageType alertMessageType)
        {
            var message = new AlertMessage(messageText, displayTime, alertMessageType);
            message.OnMessageTimeExpired += Message_OnMessageTimeExpired1;
            _alertMessages.Add(message);
            StateHasChanged();
        }

        private void Message_OnMessageTimeExpired1(object? sender, AlertMessage e)
        {
            InvokeAsync(() => {
                _alertMessages.Remove(e);
                StateHasChanged();
            });
        
       
        }

        private string GetCSSClassforType (AlertMessage message)
        {
            switch (message.MessageType) 
            {
                case AlertMessageType.Success:
                    return "alert-success";
                case AlertMessageType.Error:
                    return "alert-danger";
                case AlertMessageType.Warning:
                    return "alert-warning";
                case AlertMessageType.Information:
                    return "alert-info";
                default:
                    return "alert-success";
            }
        }


    }
}
