using Playnite.SDK;
using Playnite.SDK.Data;
using Plugin.Models;
using Plugin.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plugin
{
    public class PluginSettings : ObservableObject
    {
        private string _splashPath;

        private bool _splashEnabled;

        private bool _fullScreenOnly;

        private SplashType _splashType = SplashType.CUSTOM;

        public PluginSettings(string splashPathIn, bool splashEnabledIn)
        {
            this.SplashPath = splashPathIn;
            this.SplashEnabled = splashEnabledIn;
        }

        public string SplashPath 
        { 
            get => _splashPath; 
            set 
            {
                if (_splashPath != value)
                {
                    SetValue(ref _splashPath, value);
                    OnPropertyChanged();
                }
            } 
        }

        public bool SplashEnabled
        {
            get => _splashEnabled;
            set
            {
                if (_splashEnabled != value)
                {
                    SetValue(ref _splashEnabled, value);
                    OnPropertyChanged();
                }
            }
        }
        
        public bool FullScreenOnly
        {
            get => _fullScreenOnly;
            set
            {
                if (_fullScreenOnly != value)
                {
                    SetValue(ref _fullScreenOnly, value);
                    OnPropertyChanged();
                }
            }
        }

        public SplashType SplashType
        {
            get => _splashType;
            set
            {
                if (_splashType != value)
                {
                    SetValue(ref _splashType, value);
                    OnPropertyChanged();
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
            errors = new List<string>();
            errors.AddRange(SettingsValidator.Validate(Settings));
            return !errors.HasNonEmptyItems();
        }
    }
}