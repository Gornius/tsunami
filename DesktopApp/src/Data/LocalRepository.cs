using System.Data;
using System.Linq;
using DesktopApp.Model;
using DesktopApp.Service;
using MySqlConnector;

namespace DesktopApp.Data
{
    public class LocalRepository : ICategoryRepository, ITrendRepository
    {
        private readonly Database _database;

        public LocalRepository(Database database)
        {
            _database = database;
        }

        public void CreateCategory(Category category)
        {
            _database.Execute(
                $"INSERT INTO categories(id, title) VALUES ('{category.Id}', '{category.Title}')"
            );
        }

        public bool CategoryExists(string categoryId)
        {
            return _database.RetrieveData(
                "SELECT EXISTS (SELECT * FROM categories WHERE id = ?categoryId)",
                ParseCategoryExistsRecord,
                new[]
                {
                    new MySqlParameter("categoryId", categoryId)
                }
            ).First();
        }

        public void AddCategoryTrend(string categoryId, Trend trend)
        {
            _database.Execute(
                "INSERT INTO categories_trend(category_id, articles_count, videos_count, trend_time) " +
                $"VALUES ('{categoryId}', {trend.ArticlesCount}, {trend.VideosCount}, {trend.Date})"
            );
        }

        public void AddTagTrend(string tagTitle, Trend trend)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            var initializationSql = System.IO.File.ReadAllText(@"init.sql");
            _database.Execute(initializationSql);
        }

        private static bool ParseCategoryExistsRecord(IDataRecord record)
        {
            return record.GetBoolean(0);
        }
    }
}