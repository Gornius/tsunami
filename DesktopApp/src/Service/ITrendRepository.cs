using System.Collections.Generic;
using DesktopApp.Model;

namespace DesktopApp.Service
{
    public interface ITrendRepository
    {
        void AddCategoryTrend(string categoryId, Trend trend);
        void AddTagTrend(string tagTitle, Trend trend);
        void ReplaceCategoryTrends(Dictionary<string, Trend> categoryIdToTrend);
        List<CategoryTrend> FindAllCategoryTrends();
    }
}