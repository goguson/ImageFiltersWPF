using ImageFiltersWPF.Models;
using ImageFiltersWPF.ViewModels.Enums;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ImageFiltersWPF.ViewModels.Services
{
    public class ImageFilterService
    {
        private readonly ILogger<ImageFilterService> logger;

        Dictionary<FilterEnum, FilterBase> Filters = new Dictionary<FilterEnum, FilterBase>();
        public ImageFilterService(ILogger<ImageFilterService> logger)
        {
            this.logger = logger;
        }

        public void AddFilter(FilterEnum filterType, FilterBase filter) => Filters.Add(filterType, filter);
    }
}
