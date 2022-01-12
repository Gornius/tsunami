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
            var dataService = container.Get<DataService>();
            var database = container.Get<Database>();
            var localRepository = container.Get<LocalRepository>();
            
            database.OpenConnection();
            localRepository.Initialize();
            dataService.PopulateTags();
            database.CloseConnection();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}