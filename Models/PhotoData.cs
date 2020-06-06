using ImageFiltersWPF.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.Models
{
    public class PhotoData
    {
        public string PhotoName { get; set; }
        public string OriginalPhotoPath { get; set; }
        public string CurrentPhotoPath { get; set; }
        public List<FilterParamsBase> CurrentFilters { get; set; }
        public ImageExtensionEnum ImageFormat { get; set; }

    }
}
