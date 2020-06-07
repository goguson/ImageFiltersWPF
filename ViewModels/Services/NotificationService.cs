using Enterwell.Clients.Wpf.Notifications;
using ImageFiltersWPF.ViewModels.Enums;
using ImageFiltersWPF.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;

namespace ImageFiltersWPF.ViewModels.Services
{
    public class NotificationService : INotificationService
    {
        public ILogger<NotificationService> Logger { get; }
        public NotificationMessageManager Manager { get; }

        public NotificationService(ILogger<NotificationService> logger, NotificationMessageManager manager)
        {
            Logger = logger;
            Manager = manager;
        }

        public void ShowNotification(NotificationTypeEnum notificationType, string message)
        {
            switch (notificationType)
            {
                case NotificationTypeEnum.Information:
                    ShowInformationNotification(message);
                    break;

                case NotificationTypeEnum.Error:
                    ShowErrorNotification(message);
                    break;

                default:
                    Logger.LogError($"ShowNotification() type: {notificationType} | message: {message}");
                    ShowInformationNotification("Notification service error");
                    break;
            }
        }

        private void ShowErrorNotification(string message)
        {
            Logger.LogInformation("ShowErrorNotification()");
            Manager.CreateMessage()
            .Accent("#F15B19")
            .Background("#F15B19")
            .Animates(true)
            .AnimationInDuration(0.75)
            .AnimationOutDuration(2)
            .HasBadge("Info")
            .HasMessage($"{message}")
            .Dismiss().WithButton("Ok", button => { })
            .Queue();
        }

        private void ShowInformationNotification(string message)
        {
            Logger.LogInformation("ShowInformationNotification()");
            Manager.CreateMessage()
            .Accent("#1751C3")
            .Background("#333")
            .Animates(true)
            .AnimationInDuration(0.75)
            .AnimationOutDuration(2)
            .HasBadge("Info")
            .HasMessage($"{message}")
            .Dismiss().WithButton("Ok", button => { })
            .Queue();
        }
    }
}