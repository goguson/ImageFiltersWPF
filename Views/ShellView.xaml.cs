using ImageFiltersWPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace ImageFiltersWPF.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView(IServiceProvider serviceProvider)
        {
            DataContext = serviceProvider.GetRequiredService<ShellViewModel>();
            InitializeComponent();
        }
    }
}
