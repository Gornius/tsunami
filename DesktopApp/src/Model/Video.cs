using System.Collections.Generic;

namespace DesktopApp.Model
{
    public class Video
    {
        public IEnumerable<string> Tags { get; init; } = new List<string>();
        public string CategoryId { get; init; } = "";
    }
}