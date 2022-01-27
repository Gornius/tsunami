using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DesktopApp.Model;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;

namespace DesktopApp.Service
{
    public class YoutubePopularVideosRepository : IVideoRepository

    {
        private async Task<UserCredential> GetCredential()
        {
            await using var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read);
            
            return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                new[] { YouTubeService.Scope.Youtube },
                "user", CancellationToken.None, new FileDataStore("Books.ListMyLibrary"));
        }
        
        public List<Video> GetVideos()
        {
            UserCredential credential = GetCredential().Result;
            
            var apiService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });

            var request = apiService.Videos.List("snippet");
            request.Chart = VideosResource.ListRequest.ChartEnum.MostPopular;
            request.MaxResults = 200;
            var results = request.Execute();


            
            // TODO: Pagination
            var videoList = new List<Video>();
            foreach (var video in results.Items)
            {
                videoList.Add(new Video
                {
                    CategoryId = video.Snippet.CategoryId,
                    Tags = video.Snippet.Tags != null ? new List<string>(video.Snippet.Tags) : new List<string>(),
                });
            }

            return videoList;
        }
    }
}