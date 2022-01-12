using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DesktopApp.Service;
using DesktopApp.Ui.Avalonia;
using DesktopApp.Ui.View;

namespace DesktopApp.Ui
{
    public partial class MainWindow : Window
    {
        private ITrendRepository? _trendRepository;
        private readonly ICategoriesPopularityView _view;

        public ITrendRepository TrendRepository
        {
            set
            {
                _trendRepository = value;
                
                if (_trendRepository != null)
                    OnAttached(_trendRepository);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            _view = this.FindControl<CategoriesPopularityTab>("CategoriesTab");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnAttached(ITrendRepository trendRepository)
        {
            var categories = trendRepository.FindAllCategoryTrends();
            _view.ShowCategories(categories);
        }
    }
}