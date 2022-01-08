using System;
using System.Collections.Generic;
using DesktopApp.Model;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace DesktopApp.Service
{
    public class YoutubePopularVideosRepository : IVideoRepository

    {
        public List<Video> GetVideos()
        {
            var apiService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Environment.GetEnvironmentVariable("YOUTUBEAPIKEY")
            });

            var request = apiService.Videos.List("snippet");
            request.Chart = VideosResource.ListRequest.ChartEnum.MostPopular;
            request.MaxResults = 200;
            var results = request.Execute();


            var videoList = new List<Video>();
            foreach (var video in results.Items)
            {
                videoList.Add(new Video
                {
                    CategoryId = video.Id,
                    Tags = video.Snippet.Tags != null ? new List<string>(video.Snippet.Tags) : new List<string>(),
                });
            }

            return videoList;
        }
    }
}