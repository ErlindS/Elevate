using System.Diagnostics;

namespace Elevate
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

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

            //DispatcherQueue.GetForCurrentThread().TryEnqueue(async () =>
            //{
            //    Application.Current.DispatcherQueue.CreateTimer();
            //});
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            window.TitleBar = new TitleBar
            {
                Title = "Elevate",
                BackgroundColor = Colors.White,
                ForegroundColor = Color.FromArgb("#3B7A57"),
                HeightRequest = 40
            };

#if WINDOWS
            window.HandlerChanged += (s, e) =>
            {
                var mauiWindow = s as Window;
                if (mauiWindow?.Handler?.PlatformView is Microsoft.UI.Xaml.Window winUIWindow)
                {
                    var titleBar = winUIWindow.AppWindow.TitleBar;
                    if (titleBar != null)
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
            };
#endif

            return window;
        }
    }
}