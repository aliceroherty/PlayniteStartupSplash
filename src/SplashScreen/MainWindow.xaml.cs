using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SplashScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string PipeName = "SplashScreenPipe";

        public MainWindow()
        {
            InitializeComponent();
            StartPipeServer();
        }

        private void StartPipeServer()
        {
            Task.Run(() =>
            {
                using (var pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.In))
                {
                    pipeServer.WaitForConnection();
                    using (var reader = new StreamReader(pipeServer))
                    {
                        while (pipeServer.IsConnected)
                        {
                            var command = reader.ReadLine();
                            if (command == "Focus")
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    this.Activate();
                                    this.Focus();
                                });
                            }
                        }
                    }
                }
            });
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
