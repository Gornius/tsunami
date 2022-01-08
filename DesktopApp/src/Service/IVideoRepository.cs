using System.Collections.Generic;
using DesktopApp.Model;

namespace DesktopApp.Service
{
    public interface IVideoRepository
    {
        List<Video> GetVideos();
    }
}