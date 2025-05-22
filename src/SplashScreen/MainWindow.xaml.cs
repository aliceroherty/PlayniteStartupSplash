using LibVLCSharp.Shared;
using System;
using System.Windows;

namespace SplashScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Core.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing LibVLC: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = 0;
            this.Top = 0;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
            var args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                var videoPath = args[1];
                _libVLC = new LibVLC("--file-caching=300", "--avcodec-hw=dxva2");
                _mediaPlayer = new MediaPlayer(_libVLC);
                _mediaPlayer.Scale = 0;
                SplashVideo.MediaPlayer = _mediaPlayer;
                _mediaPlayer.EndReached += _mediaPlayer_EndReached;
                _mediaPlayer.Play(new Media(_libVLC, videoPath, FromType.FromPath));
            }

            this.Focus();
            this.Activate();
        }

        private void _mediaPlayer_EndReached(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Application.Current.Shutdown();
            });
        }
    }
}
