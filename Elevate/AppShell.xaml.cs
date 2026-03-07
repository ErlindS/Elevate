namespace Elevate
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private void OnThemeToggleClicked(object sender, EventArgs e)
        {
            if (Application.Current == null) return;

            bool switchToDark = Application.Current.UserAppTheme != AppTheme.Dark;

            if (switchToDark)
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
                if (sender is Button btn)
                    btn.Text = "☀️ Light Mode";
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Light;
                if (sender is Button btn)
                    btn.Text = "🌙 Dark Mode";
            }

            // Update TitleBar colors
            var window = Application.Current.Windows.FirstOrDefault();
            if (window != null)
            {
                App.UpdateTitleBar(window, switchToDark);

#if WINDOWS
                if (window.Handler?.PlatformView is Microsoft.UI.Xaml.Window winUIWindow)
                {
                    App.UpdateWindowsButtonColors(winUIWindow, switchToDark);
                }
#endif
            }
        }
    }
}
