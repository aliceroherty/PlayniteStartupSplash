using Gma.System.MouseKeyHook;

namespace Plugin
{
    internal class AltF4Interceptor
    {
        private static IKeyboardMouseEvents globalHook;

        public static void Start()
        {
            globalHook = Hook.GlobalEvents();
            globalHook.KeyDown += OnKeyDown;
        }

        public static void Stop()
        {
            if (globalHook != null)
            {
                globalHook.KeyDown -= OnKeyDown;
                globalHook.Dispose();
            }
        }

        private static void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // Check if Alt+F4 is pressed
            if (e.KeyCode == System.Windows.Forms.Keys.F4 && e.Alt)
            {
                e.Handled = true; // Block the key press

                // TODO: Close the splash screen and terminate Playnite
            }
        }
    }
}
