using System;
using System.Diagnostics;
using System.Windows;

namespace SplashScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the window to cover the entire screen, including the taskbar
            this.Left = 0;
            this.Top = 0;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
            this.Focus();
            this.Activate();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // Prevent the window from closing

            // Close the splash screen and terminate Playnite
            try
            {
                var playniteProcesses = Process.GetProcessesByName("Playnite");
                foreach (var process in playniteProcesses)
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to close Playnite process: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.Close(); // Close the splash screen
        }
    }
}
