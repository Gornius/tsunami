using System.IO;
using DesktopApp.Ui.View;
using System.Text.Json;
using DesktopApp.Service;

namespace DesktopApp.Ui.Presenter
{
    public class CategoriesPopularityPresenter : IPopularityPresenter
    {
        private readonly ITrendRepository _trendRepository;
        private readonly DataService _dataService;
        
        
        private ICategoriesPopularityView? _view;

        public CategoriesPopularityPresenter(ITrendRepository trendRepository, DataService dataService)
        {
            _trendRepository = trendRepository;
            _dataService = dataService;
        }

        public void Attach(ICategoriesPopularityView view)
        {
            _view = view;
            var categories = _trendRepository.FindAllCategoryTrends();
            _view.ShowCategories(categories);
        }

        public void ExportTrendsToXml()
        {
            throw new System.NotImplementedException();
            
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
            throw new System.NotImplementedException();
        }

        public void UpdateTrends()
        {
            _dataService.PopulateCategories();
            _dataService.PopulateTags();
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