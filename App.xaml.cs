using ImageFiltersWPF.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using ImageFiltersWPF.ViewModels.Services;
using Microsoft.Extensions.Hosting;
using ImageFiltersWPF.ViewModels;

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
                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Error);
                        logging.AddNLog();
                    })
                    .Build();

            ServiceProvider = host.Services;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = ServiceProvider.GetRequiredService<ShellView>();
            mainWindow.Show();
            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(ShellView));
            services.AddTransient(typeof(GalleryPageView));
            services.AddTransient(typeof(EditorPageView));

            services.AddSingleton(typeof(ShellViewModel));
            services.AddSingleton(typeof(GalleryPageViewModel));
            services.AddSingleton(typeof(EditorPageViewModel));
            services.AddSingleton(typeof(EditorPageViewModel));

            services.AddScoped<INavigationService>(serviceProvider =>
            {
                var navigationService = new NavigationService(serviceProvider);
                navigationService.Configure(PageEnum.galleryPage, typeof(GalleryPageView));
                navigationService.Configure(PageEnum.editorPage, typeof(EditorPageView));
                navigationService.MoveToPage(PageEnum.galleryPage);

                return navigationService;
            });
        }
    }
}
