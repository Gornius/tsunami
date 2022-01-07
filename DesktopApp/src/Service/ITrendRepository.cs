namespace DesktopApp
{
    public interface ITrendRepository
    {
        void AddCategoryTrend(string categoryId, Trend trend);
        void AddTagTrend(string tagTitle, Trend trend);
    }
}