using FidelidadeBE.Core.Notifications;

namespace FidelidadeBE.Core.Interfaces;

public interface INotificator
{
    bool HasNotification();
    IEnumerable<Notification> GetNotifications();
    void AddNotification(string message, NotificationType type);
}