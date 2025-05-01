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
            // 1.WMV: Windows Media Video
            // 2.AVI: Audio Video Interleave(if the appropriate codec is installed)
            // 4.MPEG: MPEG - 1 and MPEG - 2(requires appropriate codecs)
            // 5.ASF: Advanced Systems Format
            // Open file dialog to select the splash screen image
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Video Files (*.mp4;*.wmv;*.avi;*.mpeg)|*.mp4;*.wmv;*.avi;*.mpeg",
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