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
        public RelayCommand testCommand { get; set; }
        public ShellViewModel(ILogger<ShellViewModel> logger, INavigationService navigationService)
        {
            this.logger = logger;
            NavigationService = navigationService;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            testCommand = new RelayCommand((o) =>
            {
                if(NavigationService.CurrentPage.DataContext is GalleryPageViewModel)
                    NavigationService.MoveToPage(PageEnum.editorPage);
                else
                    NavigationService.MoveToPage(PageEnum.galleryPage);

            });
        }
    }
}
