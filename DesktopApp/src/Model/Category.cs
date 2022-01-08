namespace DesktopApp.Model
{
    public class Category
    {
        public string Title;
        public string Id;

        public Category(string id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}