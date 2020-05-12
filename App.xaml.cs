using ImageFiltersWPF.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using NLog;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;

namespace ImageFiltersWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            ServiceProvider = ConfigureServices(new ServiceCollection());

            var mainWindow = ServiceProvider.GetRequiredService<Shell>();
            mainWindow.Show();
        }

        private IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(Shell));
            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Error);
                logging.AddNLog();
            });
            return services.BuildServiceProvider();
        }
    }
}
