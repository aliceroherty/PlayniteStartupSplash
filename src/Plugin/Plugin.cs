using Playnite.SDK;
using Playnite.SDK.Events;
using Playnite.SDK.Plugins;
using System;
using System.Windows.Controls;

namespace Plugin
{
    public class Plugin : GenericPlugin
    {
        private PluginSettingsViewModel _settings { get; set; }

        public override Guid Id { get; } = Guid.Parse("2b363259-a65d-4c5c-a044-11160be2cbb3");

        public Plugin(IPlayniteAPI api) : base(api)
        {
            _settings = new PluginSettingsViewModel(this);
            Properties = new GenericPluginProperties
            {
                HasSettings = true
            };

            if (_settings.Settings.SplashEnabled && ((_settings.Settings.FullScreenOnly && PlayniteApi.ApplicationInfo.Mode == ApplicationMode.Fullscreen) || !_settings.Settings.FullScreenOnly))
            {
                SplashManager.Start(_settings.Settings.SplashPath);
            }
        }

        public override async void OnApplicationStarted(OnApplicationStartedEventArgs args)
        {
            if (_settings.Settings.SplashEnabled)
            {
                await SplashManager.StopAsync();
            }
        }

        public override async void OnApplicationStopped(OnApplicationStoppedEventArgs args)
        {
            await SplashManager.StopAsync();
        }

        public override ISettings GetSettings(bool firstRunSettings)
        {
            return _settings;
        }

        public override UserControl GetSettingsView(bool firstRunSettings)
        {
            return new PluginSettingsView();
        }
    }
}