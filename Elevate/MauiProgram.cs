using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
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
                    //fonts.AddFont("Mulish-Italic-VariableFont_wght.ttf", "OpenSansRegular");
                    fonts.AddFont("MulishVariableFont.ttf", "OpenSansSemibold");
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


            //Pages
            builder.Services.AddTransient<AddTaskPage>();
            builder.Services.AddTransient<CombineTaskPage>();
            //builder.Services.AddTransient<MapTaskPage>();
            builder.Services.AddTransient<TodaysTaskPage>();
            builder.Services.AddTransient<ExportPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}