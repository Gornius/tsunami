using System.Collections.Generic;

namespace DesktopApp
{
    public interface NewsRepository
    {
        int GetArticlesCount(List<string> keywords);
    }
}