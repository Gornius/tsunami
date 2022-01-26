using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Avalonia.VisualTree;
using DesktopApp.Model;
using DesktopApp.Service;
using DesktopApp.Ui.View;

namespace DesktopApp.Ui.Avalonia
{
    public class CategoriesPopularityTab : UserControl, ICategoriesPopularityView
    {
        private readonly DataGrid _categoriesDataGrid;
        private IPopularityPresenter? _popularityPresenter;
        private IErrorHandler? _errorHandler;

        public IErrorHandler ErrorHandler
        {
            set => _errorHandler = value;
        }

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

        private async Task<string> SelectFile(string extension)
        {
            var dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter {Name = "Text", Extensions = {extension}});
            dialog.AllowMultiple = true;

            var window = this.GetVisualRoot() as Window;
            if (window == null)
            {
                _errorHandler?.OnError("No window attached!");
                return "";
            }

            var result = await dialog.ShowAsync(window);
            if (result == null)
            {
                _errorHandler?.OnError("Failed to select file");
                return "";
            }

            var path = result[0];
            return path;
        }

        private void ImportCategories_OnClick(object? sender, RoutedEventArgs e)
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                var path = await SelectFile("csv");
                RequirePresenter().ImportCategoriesFromCsv(path);
            });
        }

        private void Update_OnClick(object? sender, RoutedEventArgs e)
        {
            RequirePresenter().UpdateTrends();
            
        }
    }
}