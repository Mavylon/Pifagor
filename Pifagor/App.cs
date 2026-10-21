using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pifagor.Core.Services;
using Pifagor.Core.ViewModels;
using Pifagor.Core.ViewModels.DiscreteMath;
using Pifagor.Core.ViewModels.Menu;
using Pifagor.Ui.ViewModels;
using System.Windows;

namespace Pifagor
{
    public class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            var builder = Host.CreateApplicationBuilder();

            builder.Services.AddSingleton<NavigationService>();
            builder.Services.AddSingleton<GraphPlanaryService>();

            builder.Services.AddSingleton<MainWindow>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddTransient<ProgramStartViewModel>();
            builder.Services.AddTransient<DiscreteMathMenuViewModel>();
            builder.Services.AddTransient<NetworkModelViewModel>();

            _host = builder.Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using(_host)
            {
                await _host.StopAsync();
            }
        }
    }
}
