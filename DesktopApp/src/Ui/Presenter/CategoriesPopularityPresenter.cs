using System;
using System.Collections.Generic;
using System.IO;
using DesktopApp.Ui.View;
using System.Text.Json;
using System.Xml.Serialization;
using DesktopApp.Model;
using DesktopApp.Service;

namespace DesktopApp.Ui.Presenter
{
    public class CategoriesPopularityPresenter : IPopularityPresenter
    {
        private readonly ITrendRepository _trendRepository;
        private readonly DataService _dataService;
        private readonly ICategoryRepository _categoryRepository;
        
        private ICategoriesPopularityView? _view;

        public CategoriesPopularityPresenter(ITrendRepository trendRepository, DataService dataService, ICategoryRepository categoryRepository)
        {
            _trendRepository = trendRepository;
            _dataService = dataService;
            _categoryRepository = categoryRepository;
        }

        public void Attach(ICategoriesPopularityView view)
        {
            _view = view;
            RefreshView();
        }

        private void RefreshView()
        {
            var categories = _trendRepository.FindAllCategoryTrends();
            _view?.ShowCategories(categories);
        }

        public void ExportTrendsToXml()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "export");
            var categoryTrends = _trendRepository.FindAllCategoryTrends();
            var xml = new XmlSerializer(categoryTrends.GetType());
            path = Path.Combine(path, "categoriesTrends.xml");
            
            System.IO.FileStream file = System.IO.File.Create(path);
            xml.Serialize(file, categoryTrends);
            file.Close();
        }

        public void ExportTrendsToJson()
        {
            var categoryTrends = _trendRepository.FindAllCategoryTrends();
            var json = JsonSerializer.Serialize(categoryTrends);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "export");
            Directory.CreateDirectory(path);
            path = Path.Combine(path, "categoriesTrends.json");
            var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(fs);

            sw.WriteLine(json);
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        public void ExportCategoriesToCsv()
        {
            var categories = _categoryRepository.GetAllCategories();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "export");
            Directory.CreateDirectory(path);

            path = Path.Combine(path, "categories.csv");
            var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(fs);

            sw.WriteLine("id;title");
            
            foreach (var category in categories)
            {
                sw.WriteLine(category.Id + ';' + category.Title);
            }
            
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        public void UpdateTrends()
        {
            _dataService.PopulateCategories();
            _dataService.PopulateTags();
            RefreshView();
        }

        public void ImportTrendsFromXml(string path)
        {
            throw new System.NotImplementedException();
        }

        public void ImportTrendsFromJson(string path)
        {
            throw new System.NotImplementedException();
        }

        public void ImportCategoriesFromCsv(string path)
        {
            var csvCategoryService = new CsvCategoryService(_categoryRepository);
            csvCategoryService.LoadSource(path, ';');
        }

        public void ImportTrendsFromXml()
        {
            throw new System.NotImplementedException();
        }

        public void ImportTrendsFromJson()
        {
            throw new System.NotImplementedException();
        }

        public void ImportCategoriesFromCsv()
        {
            throw new System.NotImplementedException();
        }
    }
}