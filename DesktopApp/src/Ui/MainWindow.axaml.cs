using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DesktopApp.Ui.Avalonia;
using DesktopApp.Ui.View;

namespace DesktopApp.Ui
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public CategoriesPopularityTab GetCategoriesPopularityTab()
        {
            return this.FindControl<CategoriesPopularityTab>("CategoriesTab");
        }
    }
}