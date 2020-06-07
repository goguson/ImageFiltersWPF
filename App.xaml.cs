using ImageFiltersWPF.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using ImageFiltersWPF.ViewModels.Services;
using Microsoft.Extensions.Hosting;
using ImageFiltersWPF.ViewModels;
using ImageFiltersWPF.ViewModels.Interfaces;
using ImageFiltersWPF.ViewModels.Enums;
using Enterwell.Clients.Wpf.Notifications;

namespace ImageFiltersWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        public App()
        {
            host = Host.CreateDefaultBuilder()
                    .ConfigureAppConfiguration((context, builder) =>
                    {
                        builder.AddJsonFile("appsettings.json", optional: true);
                    })
                    .ConfigureServices((services) =>
                    {
                        ConfigureServices(services);
                    })
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                        logging.AddNLog();
                    })
                    .Build();

            ServiceProvider = host.Services;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = ServiceProvider.GetRequiredService<ShellView>();
            var navigationService = ServiceProvider.GetRequiredService<NavigationService>();
            ConfigurateNavigationService(navigationService, ServiceProvider);
            ConfigureImageFormatCheckerService(ServiceProvider.GetRequiredService<ImageFormatCheckerService>());
            ConfigurateImageFilterService(ServiceProvider.GetRequiredService<ImageFilterService>(), ServiceProvider);
            navigationService.MoveToPage(PageEnum.galleryPage);
            mainWindow.Show();
            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {


            services.AddSingleton(typeof(EditorPageViewModel));
            services.AddSingleton(typeof(NavigationService));
            services.AddSingleton(typeof(ImageFormatCheckerService));
            services.AddSingleton(typeof(NotificationMessageManager));
            services.AddSingleton(typeof(ImageFilterService));
            services.AddSingleton(typeof(GaussFilterConsumer));

            services.AddSingleton<IInOutService, InOutService>();
            services.AddSingleton<IXmlManagmentService, XmlManagmentService>();
            services.AddSingleton<IPhotoViewModelFactory, PhotoViewModelFactory>();
            services.AddSingleton<INotificationService, NotificationService>();

            services.AddSingleton<INavigationService>(serviceProvider => serviceProvider.GetRequiredService<NavigationService>());
            services.AddSingleton<IImageFormatCheckerService>(serviceProvider => serviceProvider.GetRequiredService<ImageFormatCheckerService>());
            services.AddSingleton<IImageFilterService>(serviceProvider => serviceProvider.GetRequiredService<ImageFilterService>());

            services.AddTransient(typeof(ShellView));
            services.AddTransient(typeof(GalleryPageView));
            services.AddTransient(typeof(EditorPageView));

            services.AddSingleton(typeof(ShellViewModel));
            services.AddSingleton(typeof(GalleryPageViewModel));
            services.AddSingleton(typeof(EditorPageViewModel));
        }
        private void ConfigurateImageFilterService(ImageFilterService imageFilterService, IServiceProvider serviceProvider)
        {
            imageFilterService.AddFilter(FilterEnum.Gauss, serviceProvider.GetRequiredService<GaussFilterConsumer>());
        }
        private void ConfigurateNavigationService(NavigationService navigationService, IServiceProvider serviceProvider)
        {
            navigationService.AddPage(PageEnum.galleryPage, serviceProvider.GetRequiredService<GalleryPageView>());
            navigationService.AddPage(PageEnum.editorPage, serviceProvider.GetRequiredService<EditorPageView>());
        }
        private void ConfigureImageFormatCheckerService(ImageFormatCheckerService imageFormatCheckerService)
        {
            imageFormatCheckerService.AddSupportedImageExtensionType(".jpeg", ImageExtensionEnum.JPEG);
            imageFormatCheckerService.AddSupportedImageExtensionType(".jpg", ImageExtensionEnum.JPG);
            imageFormatCheckerService.AddSupportedImageExtensionType(".png", ImageExtensionEnum.PNG);
        }
    }
}
