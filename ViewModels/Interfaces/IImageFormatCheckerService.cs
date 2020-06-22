using ImageFiltersWPF.ViewModels.Enums;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    /// <summary>
    /// Interface fosupporting extenssion logic
    /// </summary>
    public interface IImageFormatCheckerService
    {
        public ImageExtensionEnum GetImageExtensionType(string dotExtension);
    }
}