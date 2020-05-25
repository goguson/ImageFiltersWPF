using ImageFiltersWPF.Models;
using ImageFiltersWPF.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.ViewModels.Services
{
    public class PhotoViewModelFactory : IPhotoViewModelFactory
    {
        private readonly ILogger<PhotoViewModelFactory> logger;

        public PhotoViewModelFactory(ILogger<PhotoViewModelFactory> logger)
        {
            this.logger = logger;
        }

        public PhotoViewModel CreatePhotoViewModel(PhotoData data)
        {
            logger.LogInformation($"CreatePhotoViewModel() || Path: {data.OriginalPhotoPath}");
            var photoViewModel = new PhotoViewModel();
            try
            {
                photoViewModel.PhotoData = data;
                photoViewModel.CurrentImage = new BitmapImage(new Uri(data.CurrentPhotoPath));
                photoViewModel.OriginalImage = new BitmapImage(new Uri(data.CurrentPhotoPath));
                return photoViewModel;
            }
            catch (Exception)
            {
                logger.LogError($"CreatePhotoViewModel() || Path: {data.OriginalPhotoPath}");
                return null;
            }
        }
    }
}
