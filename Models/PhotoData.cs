﻿using ImageFiltersWPF.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.Models
{
    public class PhotoData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string PhotoName { get; set; }
        public string OriginalPhotoPath { get; set; }
        public string CurrentPhotoPath { get; set; }

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
