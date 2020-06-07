using ImageFiltersWPF.Models;
using ImageFiltersWPF.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace ImageFiltersWPF.ViewModels.Services
{
    public class PhotoViewModelFactory : IPhotoViewModelFactory
    {
        private readonly ILogger<PhotoViewModelFactory> logger;
        private readonly IInOutService inOutService;

        public PhotoViewModelFactory(ILogger<PhotoViewModelFactory> logger, IInOutService inOutService)
        {
            this.logger = logger;
            this.inOutService = inOutService;
        }

        public PhotoViewModel CreatePhotoViewModel(PhotoData data)
        {
            logger.LogInformation($"CreatePhotoViewModel() || Path: {data.OriginalPhotoPath}");
            var photoViewModel = new PhotoViewModel();
            try
            {
                photoViewModel.PhotoData = data;
                photoViewModel.CurrentImage = inOutService.LoadImage(data.CurrentPhotoPath);
                photoViewModel.OriginalImage = inOutService.LoadImage(data.OriginalPhotoPath);
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
