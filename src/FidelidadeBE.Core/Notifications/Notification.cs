namespace FidelidadeBE.Core.Notifications;

public class Notification
{
    public string Message { get; }
    public NotificationType Type { get; }

    public Notification(string message, NotificationType type)
    {
        Message = message;
        Type = type;
    }
}