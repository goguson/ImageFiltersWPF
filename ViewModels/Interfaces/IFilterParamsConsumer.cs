using ImageFiltersWPF.Models;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    public interface IFilterParamsConsumer
    {
        public BitmapSource Consume(BitmapSource image, FilterParamsBase parameters);
    }
}