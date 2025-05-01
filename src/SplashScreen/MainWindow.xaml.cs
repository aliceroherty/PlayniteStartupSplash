using System;
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

            // Get the video path from command-line arguments
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                string videoPath = args[1];
                SplashVideo.Source = new Uri(videoPath, UriKind.Absolute);
            }

            this.Focus();
            this.Activate();
        }
    }
}
