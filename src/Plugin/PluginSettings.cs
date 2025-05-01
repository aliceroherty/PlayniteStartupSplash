using Playnite.SDK;
using Playnite.SDK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    public class PluginSettings : ObservableObject
    {
        private string splashScreenPath = string.Empty;
        private bool splashLauncherExePath = false;

        public string SplashScreenPath { get => splashScreenPath; set => SetValue(ref splashScreenPath, value); }
        public bool SplashLauncherExePath { get => splashLauncherExePath; set => SetValue(ref splashLauncherExePath, value); }
    }

    public class PluginSettingsViewModel : ObservableObject, ISettings
    {
        private readonly Plugin plugin;
        private PluginSettings editingClone { get; set; }

        private PluginSettings settings;
        public PluginSettings Settings
        {
            get => settings;
            set
            {
                settings = value;
                OnPropertyChanged();
            }
        }

        public PluginSettingsViewModel(Plugin plugin)
        {
            // Injecting your plugin instance is required for Save/Load method because Playnite saves data to a location based on what plugin requested the operation.
            this.plugin = plugin;

            // Load saved settings.
            var savedSettings = plugin.LoadPluginSettings<PluginSettings>();

            // LoadPluginSettings returns null if no saved data is available.
            if (savedSettings != null)
            {
                Settings = savedSettings;
            }
            else
            {
                Settings = new PluginSettings();
            }
        }

        public void BeginEdit()
        {
            // Code executed when settings view is opened and user starts editing values.
            editingClone = Serialization.GetClone(Settings);
        }

        public void CancelEdit()
        {
            // Code executed when user decides to cancel any changes made since BeginEdit was called.
            // This method should revert any changes made to Option1 and Option2.
            Settings = editingClone;
        }

        public void EndEdit()
        {
            // Code executed when user decides to confirm changes made since BeginEdit was called.
            // This method should save settings made to Option1 and Option2.
            plugin.SavePluginSettings(Settings);
        }

        public bool VerifySettings(out List<string> errors)
        {
            // TODO: Validate file paths
            errors = new List<string>();
            return true;
        }
    }
}