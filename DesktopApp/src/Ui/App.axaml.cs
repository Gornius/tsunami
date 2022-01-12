using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DesktopApp.Data;
using DesktopApp.Service;
using DesktopApp.Ui.Avalonia;
using DesktopApp.Ui.Presenter;

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
                var presenter = container.Get<CategoriesPopularityPresenter>();
                var view = mainWindow.GetCategoriesPopularityTab();
            
                view.Attach(presenter);
                presenter.Attach(view);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}