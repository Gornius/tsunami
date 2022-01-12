using System.IO;
using DesktopApp.Ui.View;
using System.Text.Json;
using DesktopApp.Service;

namespace DesktopApp.Ui.Presenter
{
    public class CategoriesPopularityPresenter : IPopularityPresenter
    {
        private readonly ITrendRepository _trendRepository = new DependencyInjectionContainer().Get<ITrendRepository>();
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

        public void ExportTrendsToSql()
        {
            throw new System.NotImplementedException();
        }
    }
}