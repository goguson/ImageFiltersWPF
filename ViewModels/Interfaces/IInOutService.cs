using ImageFiltersWPF.Models;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    /// <summary>
    /// Interface for inpput and aoutput logic
    /// </summary>
    public interface IInOutService
    {
        bool ImportImage(string sourcePath);
        public IEnumerable<PhotoData> LoadPhotoData();
        public BitmapImage ByteArrayToBitmapImage(byte[] array);
        public byte[] BitmapSourceToByteArray(BitmapSource bitmapImage);
        public BitmapImage LoadImage(string sourcePath);
        public bool ExportImage(PhotoViewModel photoToExport);
        public bool DeleteImage(PhotoData imageData);
    }
}