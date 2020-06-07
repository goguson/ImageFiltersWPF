using ImageFiltersWPF.Models;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    public interface IPhotoViewModelFactory
    {
        public PhotoViewModel CreatePhotoViewModel(PhotoData data);
    }
}