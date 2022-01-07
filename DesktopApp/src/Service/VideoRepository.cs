using System.Collections.Generic;

namespace DesktopApp
{
    public interface VideoRepository
    {
        List<Video> GetVideos();
    }
}