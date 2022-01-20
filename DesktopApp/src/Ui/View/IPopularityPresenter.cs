namespace DesktopApp.Ui.View
{
    public interface IPopularityPresenter
    {
        void ExportTrendsToXml();
        void ExportTrendsToJson();
        void ExportCategoriesToCsv();
        void UpdateTrends();
        void ImportTrendsFromXml();
        void ImportTrendsFromJson();
        void ImportCategoriesFromCsv();
    }
}