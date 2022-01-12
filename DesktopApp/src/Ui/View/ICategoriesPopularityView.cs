using System.Collections.Generic;
using DesktopApp.Model;

namespace DesktopApp.Ui.View
{
    public interface ICategoriesPopularityView
    {
        void ShowCategories(List<CategoryTrend> trends);
    }
}