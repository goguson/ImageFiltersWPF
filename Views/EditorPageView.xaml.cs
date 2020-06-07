using ImageFiltersWPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Controls;

namespace ImageFiltersWPF.Views
{
    /// <summary>
    /// Interaction logic for EditorPageView.xaml
    /// </summary>
    public partial class EditorPageView : Page
    {
        public EditorPageView(IServiceProvider serviceProvider)
        {
            DataContext = serviceProvider.GetRequiredService<EditorPageViewModel>();
            InitializeComponent();
        }
    }
}