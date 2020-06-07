using ImageFiltersWPF.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    public interface INotificationService
    {
        public void ShowNotification(NotificationTypeEnum notificationType, string message);
    }
}
