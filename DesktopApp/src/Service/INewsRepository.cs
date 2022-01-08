using System.Collections.Generic;

namespace DesktopApp.Service
{
    public interface INewsRepository
    {
        int GetArticlesCount(List<string> keywords);
    }
}