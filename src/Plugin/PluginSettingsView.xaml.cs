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


    }
}