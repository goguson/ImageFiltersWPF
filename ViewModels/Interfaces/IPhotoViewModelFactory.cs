using ImageFiltersWPF.Models;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    /// <summary>
    /// Interface for defining factory method
    /// </summary>
    public interface IPhotoViewModelFactory
    {
        public PhotoViewModel CreatePhotoViewModel(PhotoData data);
    }
}