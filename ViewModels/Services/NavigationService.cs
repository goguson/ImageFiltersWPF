using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace ImageFiltersWPF.ViewModels.Services
{
    public class NavigationService : INavigationService, INotifyPropertyChanged
    {

        private readonly IServiceProvider serviceProvider;

        public event PropertyChangedEventHandler PropertyChanged;
        private Page currentPage;

        public Page CurrentPage
        {
            get { return currentPage; }
            private set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        private Dictionary<PageEnum, Type> Pages { get; } = new Dictionary<PageEnum, Type>();

        public NavigationService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public void Configure(PageEnum key, Type pageType) => Pages.Add(key, pageType);
        public void MoveToPage(PageEnum pageKey, object parameter = null) => CurrentPage = GetPage(pageKey, parameter);
        private Page GetPage(PageEnum pageKey, object parameter = null)
        {
            var page = serviceProvider.GetRequiredService(Pages[pageKey]) as Page;

            if (page.DataContext is IParameters parameters)
                parameters.Activate(parameter);

            return page;
        }
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
