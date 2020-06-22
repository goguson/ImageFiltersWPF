using ImageFiltersWPF.ViewModels.Enums;
using System.Windows.Controls;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    /// <summary>
    /// Interface for UI page logic
    /// </summary>
    public interface INavigationService
    {
        public void MoveToPage(PageEnum pageKey, object parameter = null);

        public Page CurrentPage { get; }
    }
}