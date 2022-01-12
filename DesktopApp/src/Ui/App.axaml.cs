using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DesktopApp.Data;
using DesktopApp.Service;

namespace DesktopApp.Ui
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var container = new DependencyInjectionContainer();
            var database = container.Get<Database>();
            var localRepository = container.Get<LocalRepository>();
            
            database.OpenConnection();
            localRepository.Initialize();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var mainWindow = new MainWindow();
                desktop.MainWindow = mainWindow;
                mainWindow.TrendRepository = localRepository;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}