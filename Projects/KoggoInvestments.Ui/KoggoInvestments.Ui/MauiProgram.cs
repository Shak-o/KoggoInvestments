using KoggoInvestments.Ui.Models;
using KoggoInvestments.Ui.Notifications;
using KoggoInvestments.Ui.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KoggoInvestments.Ui
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
#if ANDROID
            builder.Services.AddSingleton<INotificationManagerService, KoggoInvestments.Ui.Platforms.Android.NotificationManagerService>();
#endif
            builder.Services.AddSingleton<IInvestmentApiClient, InvestmentApiClient>();

            return builder.Build();
        }
    }
}
