using ImageFiltersWPF.ViewModels.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace ImageFiltersWPF.Models
{
    /// <summary>
    /// Class is a model for image info
    /// </summary>
    public class PhotoData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string PhotoName { get; set; }
        public string OriginalPhotoPath { get; set; }
        public string CurrentPhotoPath { get; set; }
        public string ImageDataXmlPath { get; set; }
        public string DirectoryPath { get; set; }

        private List<FilterParamsBase> currentFilters;

        public List<FilterParamsBase> CurrentFilters
        {
            get { return currentFilters; }
            set { currentFilters = value; OnPropertyChanged(nameof(CurrentFilters)); }
        }

        public ImageExtensionEnum ImageFormat { get; set; }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}