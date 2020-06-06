using ImageFiltersWPF.ViewModels.Interfaces;
using ImageFiltersWPF.ViewModels.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFiltersWPF.ViewModels
{
    public class ShellViewModel
    {
        private readonly ILogger<ShellViewModel> logger;

        public INavigationService NavigationService { get; }
        public ShellViewModel(ILogger<ShellViewModel> logger, INavigationService navigationService)
        {
            this.logger = logger;
            NavigationService = navigationService;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
        }
    }
}
