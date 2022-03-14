using FidelidadeBE.Core.Interfaces;

namespace FidelidadeBE.Core.Notifications;

public class Notificator : INotificator
{
    private readonly List<Notification> _notifications;

    public Notificator()
    {
        _notifications = new List<Notification>();
    }

    public void AddNotification(string message, NotificationType type)
    {
        _notifications.Add(new Notification(message, type));
    }

    public IEnumerable<Notification> GetNotifications()
    {
        return _notifications;
    }

    public bool HasNotification()
    {
        return _notifications.Any();
    }
}