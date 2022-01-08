using DesktopApp.Model;

namespace DesktopApp.Service
{
    public interface ICategoryService
    {
        Category FetchCategoryById(string id);
    }
}