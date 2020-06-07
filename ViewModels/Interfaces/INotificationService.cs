using ImageFiltersWPF.ViewModels.Enums;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    public interface INotificationService
    {
        public void ShowNotification(NotificationTypeEnum notificationType, string message);
    }
}