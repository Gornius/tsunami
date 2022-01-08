using System;
using System.Collections.Generic;
using DesktopApp.Model;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace DesktopApp.Service
{
    public class YoutubeCategoryService : ICategoryService
    {
        private List<Category>? CategoriesList;
        public YoutubeCategoryService()
        {
            var apiService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Environment.GetEnvironmentVariable("YOUTUBEAPIKEY")
            });

            var request = apiService.VideoCategories.List("snippet");
            request.RegionCode = "US";
            var results = request.Execute();
            
            var categories = new List<Category>();
            foreach (var item in results.Items)
            {
                categories.Add(new Category()
                    {
                        Id = item.Id, 
                        Title = item.Snippet.Title
                    });
            }

            CategoriesList = categories;
        }
        public Category FetchCategoryById(string id)
        {
            return CategoriesList?.Find(category => category.Id == id) ?? throw new InvalidOperationException();
        }
    }
}