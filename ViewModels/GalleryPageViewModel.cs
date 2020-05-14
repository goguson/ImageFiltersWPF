using Microsoft.Extensions.Logging;
using System;

namespace ImageFiltersWPF.ViewModels
{
    public class GalleryPageViewModel
    {
        private readonly ILogger<GalleryPageViewModel> logger;
        public IServiceProvider ServiceProvider { get; }

        public GalleryPageViewModel(ILogger<GalleryPageViewModel> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            ServiceProvider = serviceProvider;
        }

    }
}
