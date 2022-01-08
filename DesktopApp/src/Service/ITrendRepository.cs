using DesktopApp.Model;

namespace DesktopApp.Service
{
    public interface ITrendRepository
    {
        void AddCategoryTrend(string categoryId, Trend trend);
        void AddTagTrend(string tagTitle, Trend trend);
    }
}