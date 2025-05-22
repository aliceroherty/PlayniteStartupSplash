using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Models
{
    internal static class SplashPresets
    {
        private static readonly string _pluginDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static readonly SplashVideo GAMEPLAY = new SplashVideo("Gameplay", Path.Combine(_pluginDir, "Resources\\gameplay-splash.mp4"));

        public static readonly SplashVideo LOADING_SPINNER = new SplashVideo("Loading Spinner", Path.Combine(_pluginDir, "Resources\\splash-loading-circle.mp4"));

        public static readonly SplashVideo XBOX_360 = new SplashVideo("Xbox 360", Path.Combine(_pluginDir, "Resources\\xbox-360-splash.mp4"));

        public static readonly SplashVideo PS5 = new SplashVideo("PS5", Path.Combine(_pluginDir, "Resources\\ps5-splash.mp4"));

        public static readonly List<SplashVideo> ALL = new List<SplashVideo>()
        {
            GAMEPLAY,
            LOADING_SPINNER,
            XBOX_360,
            PS5
        };
    }
}
