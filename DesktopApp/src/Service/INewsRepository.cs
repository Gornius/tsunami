using System.Collections.Generic;

namespace DesktopApp
{
    public interface INewsRepository
    {
        int GetArticlesCount(List<string> keywords);
    }
}