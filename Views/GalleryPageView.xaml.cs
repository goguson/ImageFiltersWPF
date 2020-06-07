using ImageFiltersWPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;

namespace ImageFiltersWPF.Views
{
    /// <summary>
    /// Interaction logic for GalleryPageView.xaml
    /// </summary>
    public partial class GalleryPageView : Page
    {
        public GalleryPageView(IServiceProvider serviceProvider)
        {
            DataContext = serviceProvider.GetRequiredService<GalleryPageViewModel>();
            InitializeComponent();
        }
    }
}