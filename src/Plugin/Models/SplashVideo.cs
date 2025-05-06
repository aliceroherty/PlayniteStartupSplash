using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Models
{
    public class SplashVideo
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        public SplashVideo(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
