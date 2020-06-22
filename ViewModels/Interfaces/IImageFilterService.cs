using ImageFiltersWPF.Models;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    /// <summary>
    /// Interface for declaring filters methods
    /// </summary>
    public interface IImageFilterService
    {
        public PhotoViewModel ReApplyFilters(PhotoViewModel photo);

        public BitmapSource ApplyFilter(BitmapSource photo, FilterParamsBase filter);
    }
}