using System.Diagnostics;

namespace Elevate
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // App immer im Light Mode ("white mode") starten
            UserAppTheme = AppTheme.Light;

            MainPage = new AppShell();

            // Globales Exception-Handling
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Debug.WriteLine($"UNHANDLED EXCEPTION: {e.ExceptionObject}");
            };

            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Debug.WriteLine($"TASK EXCEPTION: {e.Exception}");
            };
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            // Set initial title bar
            UpdateTitleBar(window, UserAppTheme == AppTheme.Dark);

#if WINDOWS
            window.HandlerChanged += (s, e) =>
            {
                var mauiWindow = s as Window;
                if (mauiWindow?.Handler?.PlatformView is Microsoft.UI.Xaml.Window winUIWindow)
                {
                    UpdateWindowsButtonColors(winUIWindow, UserAppTheme == AppTheme.Dark);
                }
            };
#endif

            return window;
        }

        public static void UpdateTitleBar(Window? window, bool isDark)
        {
            if (window == null) return;

            if (isDark)
            {
                window.TitleBar = new TitleBar
                {
                    Title = "Elevate",
                    BackgroundColor = Color.FromArgb("#1a1b26"),  // Tokyo Night bg
                    ForegroundColor = Color.FromArgb("#bb9af7"),  // Tokyo Night purple
                    HeightRequest = 40
                };
            }
            else
            {
                window.TitleBar = new TitleBar
                {
                    Title = "Elevate",
                    BackgroundColor = Colors.White,
                    ForegroundColor = Color.FromArgb("#3B7A57"),
                    HeightRequest = 40
                };
            }
        }

#if WINDOWS
        public static void UpdateWindowsButtonColors(Microsoft.UI.Xaml.Window winUIWindow, bool isDark)
        {
            var titleBar = winUIWindow.AppWindow.TitleBar;
            if (titleBar == null) return;

            if (isDark)
            {
                var green = global::Windows.UI.Color.FromArgb(255, 187, 154, 247);   // #bb9af7 Purple
                var hoverBg = global::Windows.UI.Color.FromArgb(255, 36, 40, 59);    // #24283b
                var inactive = global::Windows.UI.Color.FromArgb(255, 86, 95, 137);  // #565f89

                titleBar.ButtonForegroundColor = green;
                titleBar.ButtonHoverBackgroundColor = hoverBg;
                titleBar.ButtonHoverForegroundColor = green;
                titleBar.ButtonPressedBackgroundColor = hoverBg;
                titleBar.ButtonPressedForegroundColor = green;
                titleBar.ButtonInactiveForegroundColor = inactive;
            }
            else
            {
                var primaryDark = global::Windows.UI.Color.FromArgb(255, 59, 122, 87);
                var hoverBg = global::Windows.UI.Color.FromArgb(255, 235, 240, 235);

                titleBar.ButtonForegroundColor = primaryDark;
                titleBar.ButtonHoverBackgroundColor = hoverBg;
                titleBar.ButtonHoverForegroundColor = primaryDark;
                titleBar.ButtonPressedBackgroundColor = hoverBg;
                titleBar.ButtonPressedForegroundColor = primaryDark;
                titleBar.ButtonInactiveForegroundColor = global::Windows.UI.Color.FromArgb(255, 91, 168, 130);
            }
        }
#endif
    }
}