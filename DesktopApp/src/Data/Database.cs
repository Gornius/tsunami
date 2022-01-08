using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DesktopApp.Service;
using MySqlConnector;

namespace DesktopApp.Data
{
    public class Database
    {
        private readonly string _connectionString =
            "datasource=127.0.0.1;" +
            "port=3306;" +
            "database=tsunami;" +
            "username=root;" +
            $"password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
            "SslMode=none;";

        private readonly MySqlConnection _databaseConnection;

        private readonly IErrorHandler _errorHandler;

        public Database(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
            _databaseConnection = new MySqlConnection(_connectionString);
        }

        public void OpenConnection()
        {
            try
            {
                _databaseConnection.Open();
            }
            catch (Exception ex)
            {
                _errorHandler.OnError(ex.Message);
            }
        }

        public void CloseConnection()
        {
            try
            {
                _databaseConnection.Close();
            }
            catch (Exception ex)
            {
                _errorHandler.OnError(ex.Message);
            }
        }

        private MySqlCommand BuildCommand(string query)
        {
            return new(query, _databaseConnection)
            {
                CommandTimeout = 60
            };
        }

        public void Execute(string query, IEnumerable<MySqlParameter>? parameters = null)
        {
            MySqlCommand command = BuildCommand(query);
            
            if (parameters != null)
                foreach (var param in parameters)
                    command.Parameters.Add(param);
            
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _errorHandler.OnError($"Failed to execute query: {ex.Message}\n\t{query}");
            }
        }

        public List<T> RetrieveData<T>(string query, Func<IDataRecord, T> parse, IEnumerable<MySqlParameter> parameters)
        {
            MySqlCommand command = BuildCommand(query);
            
            foreach (var param in parameters)
                command.Parameters.Add(param);
            
            var results = new List<T>();

            try
            {
                using var reader = command.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                        results.Add(parse(reader));
            }
            catch (Exception ex)
            {
                _errorHandler.OnError(ex.Message);
            }

            return results;
        }
    }
}