using System.Collections.Generic;
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

        public void RemoveAllCategories()
        {
            _database.Execute("delete from categories_trend; delete from categories;");
        }

        public List<Category> GetAllCategories()
        {
            throw new System.NotImplementedException();
        }

        public void AddCategoryTrend(string categoryId, Trend trend)
        {
            _database.Execute(
                "INSERT INTO categories_trend(category_id, articles_count, videos_count, trend_date) " +
                "VALUES (?categoryId, ?articlesCount, ?videosCount, ?trendDate)",
                new[]
                {
                    new MySqlParameter("categoryId", categoryId),
                    new MySqlParameter("articlesCount", trend.ArticlesCount),
                    new MySqlParameter("videosCount", trend.VideosCount),
                    new MySqlParameter("trendDate", trend.Date)
                }
            );
        }

        public void AddTagTrend(string tagTitle, Trend trend)
        {
            _database.Execute(
                "insert into tags_trend (tag_title, articles_count, videos_count, trend_date) values (?tagTitle, ?articlesCount, ?videosCount, ?trendDate)",
                new[]
                {
                    new MySqlParameter("tagTitle", tagTitle),
                    new MySqlParameter("articlesCount", trend.ArticlesCount),
                    new MySqlParameter("videosCount", trend.VideosCount),
                    new MySqlParameter("trendDate", trend.Date)
                }
            );
        }

        public void ReplaceCategoryTrends(Dictionary<string, Trend> categoryIdToTrend)
        {
            const string query = "delete from categories_trend";
            _database.Execute(query);
            foreach (var (id, trend) in categoryIdToTrend)
            {
                AddCategoryTrend(id, trend);
            }
        }

        public List<CategoryTrend> FindAllCategoryTrends()
        {
            const string query = "select category_id, title, articles_count, videos_count, trend_date from categories inner join categories_trend ct on categories.id = ct.category_id where trend_date = CURRENT_DATE();";

            return _database.RetrieveData(query, ParseCategoryTrend, new List<MySqlParameter>());
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

        private static CategoryTrend ParseCategoryTrend(IDataRecord record)
        {
            return new CategoryTrend
            {
                Category = new Category
                {
                    Id = record.GetString(0),
                    Title = record.GetString(1)
                },
                Trend = new Trend
                {
                    ArticlesCount = record.GetInt32(2),
                    VideosCount = record.GetInt32(3),
                    Date = record.GetDateTime(4)
                }
            };
        }
    }
}