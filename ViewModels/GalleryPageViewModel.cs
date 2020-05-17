using ImageFiltersWPF.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;

namespace ImageFiltersWPF.ViewModels
{
    public class GalleryPageViewModel
    {
        private readonly ILogger<GalleryPageViewModel> logger;
        private readonly INavigationService navigationService;
        private readonly I_InOutService inOutService;

        public IServiceProvider ServiceProvider { get; }


        public RelayCommand AddNewImageCommand { get; set; }

        public GalleryPageViewModel(ILogger<GalleryPageViewModel> logger, IServiceProvider serviceProvider, INavigationService navigationService, I_InOutService inOutService)
        {
            this.logger = logger;
            ServiceProvider = serviceProvider;
            this.navigationService = navigationService;
            this.inOutService = inOutService;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            AddNewImageCommand = new RelayCommand((o) =>
            {
                OpenFileDialog dialog_window = new OpenFileDialog();
                dialog_window.Filter = "PNG Image (.png)|*.png";
                dialog_window.FilterIndex = 0;
                dialog_window.DefaultExt = "png";
                if (dialog_window.ShowDialog() != true)
                    return; //add msg box about not beign able to select photo
                var sourceFilePath = dialog_window.FileName;
                //inOutService.ImportImage(sourceFilePath) ? RefreshImageList() : ShowError();
            });
        }

        private void RefreshImageList()
        {
            throw new NotImplementedException();
        }
    }
}
