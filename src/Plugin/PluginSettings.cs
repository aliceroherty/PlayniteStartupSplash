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
        private string splashPath;
        private bool splashEnabled;

        public PluginSettings(string splashPathIn, bool splashEnabledIn)
        {
            this.SplashPath = splashPathIn;
            this.SplashEnabled = splashEnabledIn;
        }

        public string SplashPath 
        { 
            get => splashPath; 
            set 
            {
                if (splashPath != value)
                {
                    splashPath = value;
                    OnPropertyChanged(nameof(splashPath));
                }
            } 
        }

        public bool SplashEnabled
        {
            get => splashEnabled;
            set
            {
                if (splashEnabled != value)
                {
                    splashEnabled = value;
                    OnPropertyChanged(nameof(splashEnabled));
                }
            }
        }
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
                Settings = new PluginSettings(string.Empty, true);
            }
        }

        public void BeginEdit()
        {
            editingClone = Serialization.GetClone(Settings);
        }

        public void CancelEdit()
        {
            Settings = editingClone;
        }

        public void EndEdit()
        {
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