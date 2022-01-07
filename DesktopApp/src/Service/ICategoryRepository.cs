namespace DesktopApp
{
    public interface ICategoryRepository
    {
        void CreateCategory(Category category);
        bool CategoryExists(string categoryId);
    }
}