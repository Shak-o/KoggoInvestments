//using Plugin.LocalNotification;

namespace KoggoInvestments.UI.Services
{
    public class PeriodicService
    {
        private bool _isRunning;

        public void StartService()
        {
            _isRunning = true;
            Task.Run(async () =>
            {
                while (_isRunning)
                {
                    await MakeServiceRequest();
                    await Task.Delay(5000);
                }
            });
        }

        public void StopService()
        {
            _isRunning = false;
        }

        private async Task MakeServiceRequest()
        {
            try
            {
                //var result = await CallService(); ჩვენი სერვისის გამოძახება

                //if (ShouldNotifyUser(result))
                //{
                //    SendPushNotification("Service Result", "NOTIFICATION");
                //}
            }
            catch (Exception ex)
            {
            }
        }

        //private void SendPushNotification(string title, string message)
        //{
        //    var notification = new NotificationRequest
        //    {
        //        NotificationId = 77,
        //        Title = title,
        //        Description = message,
        //        ReturningData = "some data",
        //        Schedule = new NotificationRequestSchedule
        //        {
        //            NotifyTime = DateTime.Now.AddSeconds(1) // no delay
        //        }
        //    };

        //    LocalNotificationCenter.Current.Show(notification);
        //}
    }

}