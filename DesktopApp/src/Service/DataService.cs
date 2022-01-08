using System.Linq;

namespace DesktopApp.Service
{
    public class DataService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ICategoryService _categoryService;
        private readonly ICategoryRepository _categoryRepository;

        public DataService(IVideoRepository videoRepository, ICategoryService categoryService, ICategoryRepository categoryRepository)
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