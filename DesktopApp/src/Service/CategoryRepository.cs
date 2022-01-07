namespace DesktopApp
{
    public interface CategoryRepository
    {
        void CreateCategory(Category category);
        bool CategoryExists(string categoryId);
    }
}