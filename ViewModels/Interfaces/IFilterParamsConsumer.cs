using ImageFiltersWPF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    public interface IFilterParamsConsumer
    {
        public BitmapSource Consume(BitmapSource image, FilterParamsBase parameters);
    }
}
