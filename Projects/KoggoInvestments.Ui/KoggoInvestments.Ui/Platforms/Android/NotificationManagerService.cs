using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;
using KoggoInvestments.Ui.Models;
using KoggoInvestments.Ui.Notifications;

namespace KoggoInvestments.Ui.Platforms.Android
{
    public class NotificationManagerService : INotificationManagerService
    {
        const string channelId = "default";
        const string channelName = "Investments";

        public const string TitleKey = "Stock-Alerts";
        public const string MessageKey = "Alert Value reached";

        bool channelInitialized = false;
        int messageId = 0;
        int pendingIntentId = 0;

        NotificationManagerCompat compatManager;

        public event EventHandler NotificationReceived;

        public static NotificationManagerService Instance { get; private set; }

        public NotificationManagerService()
        {
            if (Instance == null)
            {
                CreateNotificationChannel();
                compatManager = NotificationManagerCompat.From(Platform.AppContext);
                Instance = this;
            }
        }

        public void SendNotification(string title, string message)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }
            ShowNotification(title, message);
            ReceiveNotification(title, message);
        }

        public void ReceiveNotification(string title, string message)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                Message = message,
            };
            NotificationReceived?.Invoke(null, args);
        }

        public void ShowNotification(string title, string message)
        {
            Intent intent = new Intent(Platform.AppContext, typeof(MainActivity));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, message);
            intent.SetFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTop);

            var pendingIntentFlags = Build.VERSION.SdkInt >= BuildVersionCodes.S
                ? PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable
                : PendingIntentFlags.UpdateCurrent;

            PendingIntent pendingIntent = PendingIntent.GetActivity(Platform.AppContext, pendingIntentId++, intent, pendingIntentFlags);
            NotificationCompat.Builder builder = new NotificationCompat.Builder(Platform.AppContext, channelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetLargeIcon(BitmapFactory.DecodeResource(Platform.AppContext.Resources, Resource.Drawable.dotnet_logo))
                .SetSmallIcon(Resource.Drawable.message_small); 

            Notification notification = builder.Build();
            compatManager.Notify(messageId++, notification);
        }

        void CreateNotificationChannel()
        {
            // Create the notification channel, but only on API 26+.
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                NotificationChannel channel = new(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = "Investment Allerts"
                };
                // Register the channel
                NotificationManager manager = (NotificationManager)Platform.AppContext.GetSystemService(Context.NotificationService);
                manager.CreateNotificationChannel(channel);
                channelInitialized = true;
            }
        }
    }
}
