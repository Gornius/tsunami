namespace DesktopApp
{
    public interface TrendRepository
    {
        void AddCategoryTrend(string categoryId, Trend trend);
        void AddTagTrend(string tagTitle, Trend trend);
    }
}