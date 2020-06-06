using ImageFiltersWPF.Models;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace ImageFiltersWPF.ViewModels
{
    public class PhotoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string imageName;

        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; OnPropertyChanged(nameof(ImageName)); }
        }

        private BitmapSource originalImage;

        public BitmapSource OriginalImage
        {
            get { return originalImage; }
            set { originalImage = value; OnPropertyChanged(nameof(OriginalImage)); }
        }
        private BitmapSource currentImage;

        public BitmapSource CurrentImage
        {
            get { return currentImage; }
            set { currentImage = value; OnPropertyChanged(nameof(CurrentImage)); }
        }

        public PhotoData PhotoData { get; set; }


        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
