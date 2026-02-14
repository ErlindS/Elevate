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
            return window;
        }
    }
}