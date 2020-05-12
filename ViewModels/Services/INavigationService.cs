using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace ImageFiltersWPF.ViewModels.Services
{
    public interface INavigationService
    {
        public void MoveToPage(PageEnum pageKey, object parameter = null);
        public Page CurrentPage { get; }
    }
}
