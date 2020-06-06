using ImageFiltersWPF.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFiltersWPF.ViewModels.Interfaces
{
    public interface IMessageBoxService
    {
        void ShowMessage(MessageBoxEnum messageType, string message);
    }
}
