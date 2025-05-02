using Playnite.SDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Utils
{
    internal static class SettingsValidator
    {
        private static readonly List<string> _errors = new List<string>();
        public static readonly List<string> AllowedVideoFormats = new List<string> { ".mp4", ".avi", ".mkv", ".mov" };

        public static List<string> Validate(PluginSettings settings)
        {
            _errors.Clear();
            ValidateFilePath(settings);
            return _errors;
        }

        private static List<string> ValidateFilePath(PluginSettings settings)
        {
            if (settings.SplashEnabled && string.IsNullOrWhiteSpace(settings.SplashPath))
            {
                _errors.Add("File path cannot be empty.");
            }
            else if (!File.Exists(settings.SplashPath))
            {
                _errors.Add($"File not found: {settings.SplashPath}");
            } else if (!AllowedVideoFormats.Contains(Path.GetExtension(settings.SplashPath).ToLower()))
            {
                _errors.Add($"Invalid file format. Allowed formats are: {string.Join(", ", AllowedVideoFormats)}");
            }

            return _errors;
        }
    }
}
