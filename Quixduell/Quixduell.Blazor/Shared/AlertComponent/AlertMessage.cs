using System.Timers;

namespace Quixduell.Blazor.Shared.AlertComponent
{
    internal class AlertMessage
    {
        public  AlertMessageType MessageType { get; set; }
        public string Message { get; set; } = default!;
        public event EventHandler<AlertMessage>? OnMessageTimeExpired;
       
        private System.Timers.Timer _timer;


        public AlertMessage(string message, TimeSpan displayTime, AlertMessageType messageType)
        {
            _timer = new System.Timers.Timer(displayTime);
            _timer.Enabled = true;
            _timer.Elapsed += Timer_Elapsed;
            Message = message;
            MessageType = messageType;
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            OnMessageTimeExpired?.Invoke(this, this);
        }
    }

    public enum AlertMessageType
    {
        Success, //"alert-succes"
        Error, //alert-dange
        Warning, //alert-warning
        Information, //alert-info
    }
}