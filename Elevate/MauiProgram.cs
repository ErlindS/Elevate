using Elevate.Services;
using Elevate.ViewModels;
using Elevate.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;

namespace Elevate
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    //fonts.AddFont("Mulish-Italic-VariableFont_wght.ttf", "OpenSansRegular");
                    fonts.AddFont("MulishVariableFont.ttf", "OpenSansSemibold");
                })
                .ConfigureMauiHandlers(handlers =>
                {
#if WINDOWS
                    ScrollViewHandler.Mapper.AppendToMapping("FixMouseWheelScroll", (handler, view) =>
                    {
                        var nativeScrollViewer = handler.PlatformView;
                        System.Diagnostics.Debug.WriteLine($"[ScrollFix] Handler invoked, PlatformView type: {nativeScrollViewer?.GetType().FullName}");
                        if (nativeScrollViewer != null)
                        {
                            nativeScrollViewer.AddHandler(
                                Microsoft.UI.Xaml.UIElement.PointerWheelChangedEvent,
                                new Microsoft.UI.Xaml.Input.PointerEventHandler((s, e) =>
                                {
                                    var properties = e.GetCurrentPoint(nativeScrollViewer).Properties;
                                    var delta = properties.MouseWheelDelta;
                                    var currentOffset = nativeScrollViewer.VerticalOffset;
                                    System.Diagnostics.Debug.WriteLine($"[ScrollFix] Wheel delta={delta}, offset={currentOffset}");
                                    nativeScrollViewer.ChangeView(null, currentOffset - delta, null);
                                    e.Handled = true;
                                }),
                                true // handledEventsToo - critical for catching events swallowed by child controls
                            );
                        }
                    });
#endif
                });

            //F1F3E0
            //D2DCB6
            //A1BC98
            //778873
            //Services
            //

            builder.Services.AddSingleton<ElevateTaskService>();
            builder.Services.AddSingleton<LiteDbService>();
            //builder.Services.AddSingleton<ElevateTimeService>();


            //ViewModels
            builder.Services.AddTransient<AddTaskViewModel>();
            builder.Services.AddTransient<CombineTaskViewModel>();
            //builder.Services.AddTransient<MapTaskViewModel>();
            builder.Services.AddTransient<TodaysTaskViewModel>();
            builder.Services.AddTransient<ExportViewModel>();
            builder.Services.AddTransient<DashboardViewModel>();
            builder.Services.AddTransient<WeeklyCalendarViewModel>();
            builder.Services.AddSingleton<CompletedHousesViewModel>();


            //Pages
            builder.Services.AddTransient<AddTaskPage>();
            builder.Services.AddTransient<CombineTaskPage>();
            //builder.Services.AddTransient<MapTaskPage>();
            builder.Services.AddTransient<TodaysTaskPage>();
            builder.Services.AddTransient<ExportPage>();
            builder.Services.AddTransient<DashboardPage>();
            builder.Services.AddTransient<WeeklyCalendarPage>();
            builder.Services.AddSingleton<CompletedHousesPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}