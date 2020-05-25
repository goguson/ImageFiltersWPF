using ImageFiltersWPF.ViewModels.Enums;
using ImageFiltersWPF.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace ImageFiltersWPF.ViewModels.Services
{
    public class NavigationService : INavigationService, INotifyPropertyChanged
    {
        private readonly ILogger<NavigationService> logger;
        public event PropertyChangedEventHandler PropertyChanged;

        private Page currentPage;

        public Page CurrentPage
        {
            get { return currentPage; }
            private set
            {
                currentPage = value;
                logger.LogInformation($"CurrentPage() - {nameof(CurrentPage)}");
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        private Dictionary<PageEnum, Page> Pages { get; } = new Dictionary<PageEnum, Page>();

        public NavigationService(ILogger<NavigationService> logger)
        {
            this.logger = logger;
        }
        public void AddPage(PageEnum key, Page page) => Pages.Add(key, page);
        public void MoveToPage(PageEnum pageKey, object parameter = null)
        {
            logger.LogInformation($"MoveToPage() => {pageKey}");
            CurrentPage = GetPage(pageKey, parameter);
        }
        private Page GetPage(PageEnum pageKey, object parameter = null)
        {
            logger.LogInformation($"GetPage() {pageKey}");
            var page = Pages[pageKey];

            if (page.DataContext is IParameters parameters)
                parameters.Activate(parameter);

            return page;
        }
        private void OnPropertyChanged(string propertyName)
        {
            logger.LogInformation($"OnPropertyChanged => {propertyName}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
