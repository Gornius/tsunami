using System;
using System.Collections.Generic;
using System.IO;
using DesktopApp.Model;

namespace DesktopApp.Service
{
    public class CsvCategoryService : ICategoryService
    {
        private Dictionary<string, Category> categoriesDic = new();
        public Category FetchCategoryById(string id)
        {
            throw new System.NotImplementedException();
        }

        public void LoadSource(string path, char separator=';')
        {
            using var reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                // Skip header line
                reader.ReadLine();
                var line = reader.ReadLine()?.Split(separator);
                categoriesDic.Add(line[0], new Category()
                {
                    Id = line[0],
                    Title = line[1],
                });
            }
        }
    }
}