using Playnite.SDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Plugin
{
    internal class SplashManager
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private static Process splashProcess { get; set; }

        // Timer to monitor focus (Playnite can steal focus when it starts breaking the Alt+F4 functionality this workaround fixes it)
        private static DispatcherTimer focusTimer { get; set; } = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(500)
        };

        private static readonly ILogger logger = LogManager.GetLogger("Startup Splash Screen");

        public static bool IsSplashScreenVisible
        {
            get
            {
                return splashProcess != null && !splashProcess.HasExited;
            }
        }

        public static void Start(string splashResourcePath)
        {
            Task.Run(() =>
            {
                // Start the splash screen process
                try
                {
                    // Path to the splash screen executable
                    string splashScreenExecutablePath = @"C:\Users\Alice\Documents\Playnite\Plugins\PlayniteStartupSplash\src\SplashScreen\bin\Debug\SplashScreen.exe";

                    // Ensure Splash Screen exe exists before starting
                    if (!System.IO.File.Exists(splashScreenExecutablePath))
                    {
                        logger.Error($"Splash screen executable not found at {splashScreenExecutablePath}");
                        return;
                    }

                    logger.Info($"Attempting to start splash screen from path: {splashScreenExecutablePath}");
                    splashProcess = Process.Start(splashScreenExecutablePath);

                    // Begin polling for focus
                    FocusSplashScreen();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Failed to start splash screen process.");
                }
            });
        }

        public static async Task StopAsync()
        {
            // Wait two seconds to give the UI some extra time to render
            await Task.Delay(5000);

            // Stop polling for focus
            RelinquishFocus();

            // Close the splash screen process
            if (splashProcess != null && !splashProcess.HasExited)
            {
                splashProcess.Kill();
            }
        }

        private static void FocusSplashScreen(int retryCount = 0)
        {
            const int maxRetries = 20;
            IntPtr mainWindowHandle = splashProcess.MainWindowHandle;

            // Bring the splash screen process to the foreground
            if (splashProcess != null && mainWindowHandle != IntPtr.Zero)
            {
                SetForegroundWindow(splashProcess.MainWindowHandle);

                // Poll for focus every 500ms
                focusTimer.Tick += (s, args) =>
                {
                    SetForegroundWindow(splashProcess.MainWindowHandle);
                };

                // Start polling for focus
                focusTimer.Start();
            }
            else
            {
                if (retryCount >= maxRetries)
                {
                    logger.Error($"Failed to focus on the splash screen after max attempts ({maxRetries} attempts). Process may have failed to start.");
                    return;
                }
                else
                {
                    // Wait for the splash screen process to initialize
                    Thread.Sleep(50);
                    FocusSplashScreen(retryCount + 1);
                }
            }
        }

        private static void RelinquishFocus()
        {
            focusTimer.Stop();
        }
    }
}
