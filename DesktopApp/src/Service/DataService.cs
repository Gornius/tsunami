using System.Linq;

namespace DesktopApp
{
    public class DataService
    {
        private readonly VideoRepository _videoRepository;
        private readonly CategoryService _categoryService;
        private readonly CategoryRepository _categoryRepository;

        public DataService(VideoRepository videoRepository, CategoryService categoryService, CategoryRepository categoryRepository)
        {
            _videoRepository = videoRepository;
            _categoryService = categoryService;
            _categoryRepository = categoryRepository;
        }

        void PopulateCategories()
        {
            var nonExistingCategories = _videoRepository.GetVideos()
                .Select(video => video.CategoryId)
                .Distinct()
                .Where(categoryId => _categoryRepository.CategoryExists(categoryId));

            nonExistingCategories
                .Select(categoryId => _categoryService.FetchCategoryById(categoryId))
                .ToList()
                .ForEach(category => _categoryRepository.CreateCategory(category));
        }
    }
}