using ImageFiltersWPF.Models;
using ImageFiltersWPF.ViewModels.Enums;
using ImageFiltersWPF.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.ViewModels.Services
{
    public class ImageFilterService : IImageFilterService
    {
        private readonly ILogger<ImageFilterService> logger;

        private Dictionary<FilterEnum, IFilterParamsConsumer> Filters { get; } = new Dictionary<FilterEnum, IFilterParamsConsumer>();

        public ImageFilterService(ILogger<ImageFilterService> logger)
        {
            this.logger = logger;
        }

        public void AddFilter(FilterEnum filterType, IFilterParamsConsumer implementation) => Filters.Add(filterType, implementation);

        public PhotoViewModel ReApplyFilters(PhotoViewModel photo)
        {
            logger.LogInformation("ReApplyFilters()");
            photo.CurrentImage = photo.OriginalImage.CloneCurrentValue();
            foreach (var filter in photo.PhotoData.CurrentFilters)
            {
                photo.CurrentImage = ApplyFilterToImage(photo.CurrentImage, filter);
            }
            return photo;
        }

        public BitmapSource ApplyFilter(BitmapSource photo, FilterParamsBase filter)
        {
            logger.LogInformation("ApplyFilters()");
            return ApplyFilterToImage(photo, filter);
        }

        private BitmapSource ApplyFilterToImage(BitmapSource image, FilterParamsBase filterParams)
        {
            if (filterParams is GaussFilterParams)
                return Filters[FilterEnum.Gauss].Consume(image, filterParams);
            return null;
        }
    }
}