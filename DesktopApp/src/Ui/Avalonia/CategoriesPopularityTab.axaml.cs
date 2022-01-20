using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DesktopApp.Model;
using DesktopApp.Ui.View;

namespace DesktopApp.Ui.Avalonia
{
    public class CategoriesPopularityTab : UserControl, ICategoriesPopularityView
    {
        private readonly DataGrid _categoriesDataGrid;
        private IPopularityPresenter? _popularityPresenter;
        
        public CategoriesPopularityTab()
        {
            InitializeComponent();
            _categoriesDataGrid = this.FindControl<DataGrid>("CategoriesDataGrid");
            _categoriesDataGrid.IsReadOnly = true;
        }

        public void Attach(IPopularityPresenter presenter)
        {
            _popularityPresenter = presenter;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void ShowCategories(List<CategoryTrend> trends)
        {
            _categoriesDataGrid.Items = trends.Select(CategoryPopularityRecord.FromCategoryTrend);
        }

        private IPopularityPresenter RequirePresenter()
        {
            if (_popularityPresenter == null)
                throw new ApplicationException("Presenter not attached!");

            return _popularityPresenter;
        }

        private void ExportJson_OnClick(object? sender, RoutedEventArgs e)
        {
            RequirePresenter().ExportTrendsToJson();
        }

        private void ExportXml_OnClick(object? sender, RoutedEventArgs e)
        {
            RequirePresenter().ExportTrendsToXml();
        }

        private void ExportSql_OnClick(object? sender, RoutedEventArgs e)
        {
            RequirePresenter().ExportCategoriesToCsv();
        }
    }
}