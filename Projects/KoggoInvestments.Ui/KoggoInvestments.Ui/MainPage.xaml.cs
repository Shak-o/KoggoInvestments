using KoggoInvestments.Ui.Models;
using KoggoInvestments.Ui.Notifications;
#if ANDROID
using KoggoInvestments.Ui.Platforms.Android;
#endif

using Microsoft.Maui.Controls;

namespace KoggoInvestments.Ui
{
    public partial class MainPage : ContentPage
    {

        INotificationManagerService notificationManager;
        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(INotificationManagerService manager)
        {
            InitializeComponent();

            notificationManager = manager;
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var eventData = (NotificationEventArgs)eventArgs;
                ShowNotification(eventData.Title, eventData.Message);
            };
        }

#if ANDROID
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        PermissionStatus status = await Permissions.RequestAsync<NotificationPermission>();
    }
#endif

        void ShowNotification(string title, string message)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };

                verticalStackLayout.Children.Add(msg);
            });
        }
    }

}
