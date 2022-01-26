using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using DesktopApp.Model;
using DesktopApp.Ui;

namespace DesktopApp.Service
{
    public class CsvCategoryService : ICategoryService
    {
        private Dictionary<string, Category> categoriesDic = new();
        private readonly ICategoryRepository _categoryRepository;

        public CsvCategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category FetchCategoryById(string id)
        {
            return categoriesDic[id];
        }

        public void LoadSource(string path, char separator=';')
        {
            using var reader = new StreamReader(path);
            
            // Skip header line
            reader.ReadLine();
            
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine()?.Split(separator);
                categoriesDic.Add(line[0], new Category()
                {
                    Id = line[0],
                    Title = line[1],
                });
            }
            
            _categoryRepository.RemoveAllCategories();
            foreach (var (Id, Cat) in categoriesDic)
            {
                _categoryRepository.CreateCategory(Cat);
            }
        }
    }
}