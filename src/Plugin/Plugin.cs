using Playnite.SDK;
using Playnite.SDK.Events;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Plugin
{
    public class Plugin : GenericPlugin
    {
        private static readonly ILogger logger = LogManager.GetLogger("Startup Splash Screen");

        private PluginSettingsViewModel settings { get; set; }

        public override Guid Id { get; } = Guid.Parse("2b363259-a65d-4c5c-a044-11160be2cbb3");

        public Plugin(IPlayniteAPI api) : base(api)
        {
            settings = new PluginSettingsViewModel(this);
            Properties = new GenericPluginProperties
            {
                HasSettings = true
            };

            if (settings.Settings.SplashEnabled)
            {
                // Display splash screen
                SplashManager.Start(settings.Settings.SplashPath);
            }
        }

        public override async void OnApplicationStarted(OnApplicationStartedEventArgs args)
        {
            if (settings.Settings.SplashEnabled)
            {
                // Hide splash screen
                await SplashManager.StopAsync();
            }
        }

        public override async void OnApplicationStopped(OnApplicationStoppedEventArgs args)
        {
            // Close splash screen if Playnite is closed before it is hidden
            if (SplashManager.SplashProcess != null && !SplashManager.SplashProcess.HasExited)
            {
                await SplashManager.StopAsync();
            }
        }

        public override ISettings GetSettings(bool firstRunSettings)
        {
            return settings;
        }

        public override UserControl GetSettingsView(bool firstRunSettings)
        {
            return new PluginSettingsView();
        }
    }
}