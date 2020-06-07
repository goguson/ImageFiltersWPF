using ImageFiltersWPF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    public interface IImageFilterService
    {
        public PhotoViewModel ReApplyFilters(PhotoViewModel photo);
        public BitmapSource ApplyFilter(BitmapSource photo, FilterParamsBase filter);
    }
}
