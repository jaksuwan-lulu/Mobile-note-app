using dululu.DataServices;
using dululu.ViewModels;
using dululu.Views;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using CommunityToolkit.Maui;

namespace dululu
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureSyncfusionCore().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("NotoSansThai_Condensed-Regular.ttf", "NotoSansThai");
            }).UseMauiCommunityToolkit();
            builder.Services.AddTransient<LBookService, BookService>();
            builder.Services.AddTransient<AddOrUpdateBookPageViewmodel>();
            builder.Services.AddTransient<AddOrUpdateBookPage>();
            builder.Services.AddTransient<BooklistHomePageViewmodel>();
            builder.Services.AddTransient<BooklistHomePage>();
            builder.Services.AddTransient<BookDetailsPageViewmodel>();
            builder.Services.AddTransient<BookDetailsPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}