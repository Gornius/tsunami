using System.Collections.Generic;

namespace DesktopApp
{
    public interface IVideoRepository
    {
        List<Video> GetVideos();
    }
}