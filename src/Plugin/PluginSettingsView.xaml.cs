using Plugin.Models;
using Plugin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Controls.ComboBox;

namespace Plugin
{
    public partial class PluginSettingsView : UserControl
    {
        public PluginSettingsView()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is PluginSettingsViewModel viewModel)
            {
                var presetPicker = PresetPicker;
                if (presetPicker != null && presetPicker.ItemsSource == null)
                {
                    presetPicker.ItemsSource = SplashPresets.ALL;
                    presetPicker.DisplayMemberPath = "Name";
                    presetPicker.SelectedValuePath = "Path";
                }

                if (viewModel.Settings.SplashType == SplashType.CUSTOM)
                {
                    CustomRadioButton.IsChecked = true;
                    CustomVideoPickerSection.Visibility = Visibility.Visible;
                    PresetPickerSection.Visibility = Visibility.Collapsed;
                }
                else if (viewModel.Settings.SplashType == SplashType.PRESET)
                {
                    PresetRadioButton.IsChecked = true;
                    CustomVideoPickerSection.Visibility = Visibility.Collapsed;
                    PresetPickerSection.Visibility = Visibility.Visible;

                    var selectedPreset = SplashPresets.ALL.FirstOrDefault(preset => preset.Path == viewModel.Settings.SplashPath);
                    PresetPicker.SelectedIndex = SplashPresets.ALL.IndexOf(selectedPreset);
                }
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var allowedFormats = string.Join(";", SettingsValidator.AllowedVideoFormats.Select(ext => $"*{ext}"));
            var filter = $"Video Files ({allowedFormats})|{allowedFormats}";

            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = filter,
                Title = "Select Splash Screen Image"
            };

            if (dialog.ShowDialog() == true)
            {
                // Set the selected file path to the SplashScreenPath property
                var viewModel = (PluginSettingsViewModel)DataContext;
                viewModel.Settings.SplashPath = dialog.FileName;
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                var viewModel = (PluginSettingsViewModel)DataContext;

                if (radioButton.Name == "CustomRadioButton")
                {
                    CustomVideoPickerSection.Visibility = Visibility.Visible;
                    PresetPickerSection.Visibility = Visibility.Collapsed;
                    viewModel.Settings.SplashType = SplashType.CUSTOM;
                }
                else if (radioButton.Name == "PresetRadioButton")
                {
                    CustomVideoPickerSection.Visibility = Visibility.Collapsed;
                    PresetPickerSection.Visibility = Visibility.Visible;
                    viewModel.Settings.SplashType = SplashType.PRESET;
                }
            }
        }

        private void PresetPicker_SelectionChanged(object sender, RoutedEventArgs e)
        {
            SplashVideo selectedPreset = (SplashVideo)((ComboBox)sender).SelectedItem;
            if (selectedPreset != null)
            {
                var viewModel = (PluginSettingsViewModel)DataContext;
                viewModel.Settings.SplashPath = selectedPreset.Path;
            }
        }
    }
}