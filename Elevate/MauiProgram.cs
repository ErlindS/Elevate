using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using MindFusion.Scheduling;
using Elevate.Services;
using Elevate.ViewModels;

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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            //Services
            builder.Services.AddSingleton<ElevateTaskService>();


            //ViewModels
            builder.Services.AddTransient<AddTaskViewModel>();
            builder.Services.AddTransient<AnalyticsViewModel>();
            builder.Services.AddTransient<CombineTaskViewModel>();
            builder.Services.AddTransient<MapTaskViewModel>();
            builder.Services.AddTransient<TodaysTaskViewModel>();


            //Pages
            builder.Services.AddTransient<AddTaskPage>();
            builder.Services.AddTransient<AnalyticsPage>();
            builder.Services.AddTransient<CombineTaskPage>();
            builder.Services.AddTransient<MapTaskPage>();
            builder.Services.AddTransient<TodaysTaskPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}