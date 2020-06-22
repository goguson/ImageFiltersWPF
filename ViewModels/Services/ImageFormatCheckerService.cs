using ImageFiltersWPF.ViewModels.Enums;
using ImageFiltersWPF.ViewModels.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ImageFiltersWPF.ViewModels.Services
{
    /// <summary>
    /// Serivce for checking image format from provided string
    /// </summary>
    public class ImageFormatCheckerService : IImageFormatCheckerService
    {
        private readonly ILogger<ImageFormatCheckerService> logger;

        private Dictionary<string, ImageExtensionEnum> ImageExtensions = new Dictionary<string, ImageExtensionEnum>();

        public ImageFormatCheckerService(ILogger<ImageFormatCheckerService> logger)
        {
            this.logger = logger;
        }

        public void AddSupportedImageExtensionType(string dotExtension, ImageExtensionEnum enumerableRepresentation) => ImageExtensions.Add(dotExtension, enumerableRepresentation);

        public ImageExtensionEnum GetImageExtensionType(string dotExtension)
        {
            logger.LogInformation($"GetImageExtensionType({dotExtension})");
            return ImageExtensions[dotExtension];
        }
    }
}