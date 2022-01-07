using System.Data;
using System.Linq;
using MySqlConnector;

namespace DesktopApp.Data
{
    public class LocalRepository : ICategoryRepository
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
                new []
                {
                    new MySqlParameter("categoryId", categoryId)
                }
            ).First();
        }

        private static bool ParseCategoryExistsRecord(IDataRecord record)
        {
            return record.GetBoolean(0);
        }

        public void Initialize()
        {
            var initializationSql = System.IO.File.ReadAllText(@"init.sql");
            _database.Execute(initializationSql);
        }
    }
}