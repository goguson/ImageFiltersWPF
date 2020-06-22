using ImageFiltersWPF.Models;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    /// <summary>
    /// Interface for declaring Consume method on filters
    /// </summary>
    public interface IFilterParamsConsumer
    {
        public BitmapSource Consume(BitmapSource image, FilterParamsBase parameters);
    }
}