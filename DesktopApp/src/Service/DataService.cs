using System;
using System.Collections.Generic;
using System.Linq;
using DesktopApp.Model;

namespace DesktopApp.Service
{
    public class DataService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly INewsRepository _newsRepository;
        private readonly ITrendRepository _trendRepository;

        public DataService(IVideoRepository videoRepository, ICategoryService categoryService,
            ICategoryRepository categoryRepository, INewsRepository newsRepository, ITrendRepository trendRepository)
        {
            _videoRepository = videoRepository;
            _categoryService = categoryService;
            _categoryRepository = categoryRepository;
            _newsRepository = newsRepository;
            _trendRepository = trendRepository;
        }

        public void PopulateCategories()
        {
            var videos = _videoRepository.GetVideos();

            var nonExistingCategories = videos
                .Select(video => video.CategoryId)
                .Distinct()
                .Where(categoryId => !_categoryRepository.CategoryExists(categoryId));

            nonExistingCategories
                .Select(categoryId => _categoryService.FetchCategoryById(categoryId))
                .ToList()
                .ForEach(category => _categoryRepository.CreateCategory(category));

            var categoryIdsToCounts = videos
                .Select(video => video.CategoryId)
                .Aggregate(new Dictionary<string, int>(), IncrementItemOccurrence);

            var articlesCounts = videos
                .Select(video => video.CategoryId)
                .Distinct()
                .Select(categoryId => _categoryService.FetchCategoryById(categoryId))
                .Select(category => _newsRepository.GetArticlesCount(new List<string>(new[] {category.Title})));

            categoryIdsToCounts.Zip(articlesCounts)
                .Select(MakeTrend)
                .ToList()
                .ForEach(entry =>
                {
                    var (id, trend) = entry;
                    _trendRepository.AddCategoryTrend(id, trend);
                });
        }

        public void PopulateTags()
        {
            var tags = _videoRepository.GetVideos()
                .Select(video => video.Tags)
                .SelectMany(list => list)
                .ToList();
            
            var tagsToVideosCounts = tags
                .Aggregate(new Dictionary<string, int>(), IncrementItemOccurrence);
            
            var articlesCounts = tags
                .Distinct()
                .Select(tag => _newsRepository.GetArticlesCount(new List<string>(new[] {tag})));
            
            tagsToVideosCounts.Zip(articlesCounts)
                .Select(MakeTrend)
                .ToList()
                .ForEach(entry =>
                {
                    var (tag, trend) = entry;
                    _trendRepository.AddTagTrend(tag, trend);
                });
        }

        private static (string, Trend) MakeTrend((KeyValuePair<string, int>, int) entry)
        {
            var (tagToVideosCount, articlesCount) = entry;
            var (tag, videosCount) = tagToVideosCount;
            var trend = new Trend {
                ArticlesCount = articlesCount,
                VideosCount = videosCount,
                Date = DateTime.Today
            };

            return (tag, trend);
        }
        
        private static Dictionary<string, int> IncrementItemOccurrence(Dictionary<string, int> itemsToOccurrences, string item)
        {
            if (!itemsToOccurrences.ContainsKey(item))
                itemsToOccurrences.Add(item, 0);
            else
                itemsToOccurrences[item] += 1;

            return itemsToOccurrences;
        }
    }
}