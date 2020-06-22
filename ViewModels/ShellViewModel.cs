using Enterwell.Clients.Wpf.Notifications;
using ImageFiltersWPF.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;

namespace ImageFiltersWPF.ViewModels
{
    /// <summary>
    /// Class responsible for logic behind window frame
    /// </summary>
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