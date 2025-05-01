using Playnite.SDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin
{
    internal class SplashManager
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private static Process splashProcess { get; set; }

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

                    // Wait for the splash screen process to initialize
                    Thread.Sleep(500); // Adjust as needed

                    // Bring the splash screen process to the foreground
                    if (splashProcess != null)
                    {
                        SetForegroundWindow(splashProcess.MainWindowHandle);
                    }
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

            // Close the splash screen process
            if (splashProcess != null && !splashProcess.HasExited)
            {
                splashProcess.Kill();
            }
        }
    }
}
