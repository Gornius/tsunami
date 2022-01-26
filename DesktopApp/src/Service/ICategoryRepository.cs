using System.Collections.Generic;
using DesktopApp.Model;

namespace DesktopApp.Service
{
    public interface ICategoryRepository
    {
        void CreateCategory(Category category);
        bool CategoryExists(string categoryId);
        void RemoveAllCategories();
        List<Category> GetAllCategories();
    }
}