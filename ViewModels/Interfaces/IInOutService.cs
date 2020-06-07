using ImageFiltersWPF.Models;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    public interface IInOutService
    {
        bool ImportImage(string sourcePath);
        public IEnumerable<PhotoData> LoadPhotoData();

        public BitmapImage LoadImage(string sourcePath);
        public bool ExportImage(PhotoViewModel photoToExport);
        public bool DeleteImage(PhotoData imageData);
    }
}
