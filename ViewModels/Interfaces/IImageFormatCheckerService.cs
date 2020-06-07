using ImageFiltersWPF.ViewModels.Enums;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    public interface IImageFormatCheckerService
    {
        public ImageExtensionEnum GetImageExtensionType(string dotExtension);
    }
}