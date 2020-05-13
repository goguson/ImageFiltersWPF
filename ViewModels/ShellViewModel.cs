using ImageFiltersWPF.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFiltersWPF.ViewModels
{
    public class ShellViewModel
    {
        public INavigationService NavigationService { get; }
        public RelayCommand testCommand { get; set; }
        public ShellViewModel(INavigationService navigationService)
        {
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
