using Playnite.SDK;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Plugin
{
    internal class SplashManager
    {
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
                    // Setup interceptor to block alt+f4 from closing other windows
                    AltF4Interceptor.Start();

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
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Failed to start splash screen process.");
                }
            });
        }

        public static async Task StopAsync()
        {
            // TODO: This delay is a hacky solution, find a better way to do this maybe poll for playnites process to check when UI is done rendering
            // Wait two seconds to give the UI some extra time to render
            await Task.Delay(5000);

            // Close the splash screen process
            if (splashProcess != null && !splashProcess.HasExited)
            {
                splashProcess.Kill();
            }

            // Stop interceptor that blocks alt+f4 from closing other windows
            AltF4Interceptor.Stop();
        }
    }
}
