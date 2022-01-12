using DesktopApp.Model;

namespace DesktopApp.Ui.Avalonia
{
    public class CategoryPopularityRecord
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public int ArticlesCount { get; init; }
        public int VideosCount { get; init; }
        
        public static CategoryPopularityRecord FromCategoryTrend(CategoryTrend categoryTrend)
        {
            return new CategoryPopularityRecord
            {
                Id = categoryTrend.Category.Id,
                Name = categoryTrend.Category.Title,
                ArticlesCount = categoryTrend.Trend.ArticlesCount,
                VideosCount = categoryTrend.Trend.VideosCount
            };
        }
    }
}