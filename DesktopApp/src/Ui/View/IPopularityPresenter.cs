namespace DesktopApp.Ui.View
{
    public interface IPopularityPresenter
    {
        void ExportTrendsToXml();
        void ExportTrendsToJson();
        void ExportCategoriesToCsv();
        void UpdateTrends();
        void ImportTrendsFromXml(string path);
        void ImportTrendsFromJson(string path);
        void ImportCategoriesFromCsv(string path);
        void OnRefresh();
    }
}