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

            if (Application.Current.UserAppTheme == AppTheme.Dark)
            {
                Application.Current.UserAppTheme = AppTheme.Light;
                if (sender is Button btn)
                    btn.Text = "🌙 Dark Mode";
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
                if (sender is Button btn)
                    btn.Text = "☀️ Light Mode";
            }
        }
    }
}
