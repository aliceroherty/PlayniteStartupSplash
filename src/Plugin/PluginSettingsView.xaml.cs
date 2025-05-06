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

        private void PresetPicker_GotMouseCapture(object sender, MouseEventArgs e)
        {
            ComboBox presetPicker = sender as ComboBox;

            if (presetPicker != null && presetPicker.ItemsSource == null)
            {
                presetPicker.ItemsSource = (DataContext as PluginSettingsViewModel).Settings.SplashPresets;
                presetPicker.DisplayMemberPath = "Name";
                presetPicker.SelectedValuePath = "Path";
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (radioButton.Name == "CustomRadioButton")
                {
                    CustomVideoPickerSection.Visibility = Visibility.Visible;
                    PresetPickerSection.Visibility = Visibility.Collapsed;
                }
                else if (radioButton.Name == "PresetRadioButton")
                {
                    CustomVideoPickerSection.Visibility = Visibility.Collapsed;
                    PresetPickerSection.Visibility = Visibility.Visible;
                }
            }
        }
    }
}