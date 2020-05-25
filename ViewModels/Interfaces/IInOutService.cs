using ImageFiltersWPF.Models;
using System.Collections.Generic;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    public interface IInOutService
    {
        bool ImportImage(string sourcePath);
        public IEnumerable<PhotoData> LoadPhotoData();
    }
}
