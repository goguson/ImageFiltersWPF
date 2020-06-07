using Enterwell.Clients.Wpf.Notifications;
using ImageFiltersWPF.ViewModels.Interfaces;
using ImageFiltersWPF.ViewModels.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFiltersWPF.ViewModels
{
    public class ShellViewModel
    {
        private readonly ILogger<ShellViewModel> logger;

        public INavigationService NavigationService { get; }
        public NotificationMessageManager Manager { get; set; }
        public ShellViewModel(ILogger<ShellViewModel> logger, INavigationService navigationService, NotificationMessageManager manager)
        {
            this.logger = logger;
            NavigationService = navigationService;
            Manager = manager;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
        }
    }
}
