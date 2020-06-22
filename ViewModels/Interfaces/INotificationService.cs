using ImageFiltersWPF.ViewModels.Enums;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    /// <summary>
    /// interface for showing notifications
    /// </summary>
    public interface INotificationService
    {
        public void ShowNotification(NotificationTypeEnum notificationType, string message);
    }
}