using DesktopApp.Model;

namespace DesktopApp.Service
{
    public interface ICategoryRepository
    {
        void CreateCategory(Category category);
        bool CategoryExists(string categoryId);
    }
}