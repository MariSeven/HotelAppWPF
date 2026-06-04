using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HotelAppWPF
{
    public partial class App : Application
    {
        public static bool IsDarkTheme { get; set; } = false;

        public static void ApplyTheme()
        {
            var app = Current as App;
            app?.UpdateAllWindows();
        }

        private void UpdateAllWindows()
        {
            foreach (Window window in Application.Current.Windows)
            {
                UpdateWindowTheme(window);
            }
        }

        private void UpdateWindowTheme(Window window)
        {
            if (window is MainWindow mainWindow)
            {
                mainWindow.UpdateTheme();
            }
            else if (window is account accountWindow)
            {
                accountWindow.UpdateTheme();
            }
        }
    }
}