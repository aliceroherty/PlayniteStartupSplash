using Gma.System.MouseKeyHook;
using System.Windows;

namespace Plugin
{
    // FIX: This interceptor doesnt seem to be functioning as expected
    internal class AltF4Interceptor
    {
        private static IKeyboardMouseEvents _globalHook;

        public static void Start()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += OnKeyDown;
        }

        public static void Stop()
        {
            if (_globalHook != null)
            {
                _globalHook.KeyDown -= OnKeyDown;
                _globalHook.Dispose();
            }
        }

        private static void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // Check if Alt+F4 is pressed
            if (e.KeyCode == System.Windows.Forms.Keys.F4 && e.Alt)
            {
                // Block the key press
                e.Handled = true;
                SplashManager.StopAsync().RunSynchronously();
            }
        }
    }
}
