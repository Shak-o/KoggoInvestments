//using Plugin.LocalNotification;

using KoggoInvestments.Ui;
using KoggoInvestments.Ui.Models;
using KoggoInvestments.Ui.Notifications;
using KoggoInvestments.Ui.Services;
using System.Threading;

namespace KoggoInvestments.UI.Services
{
    public class PeriodicService
    {
        private readonly INotificationManagerService notificationManagerService;
        private readonly IInvestmentApiClient apiClient;
        private  CancellationTokenSource _cancellationTokenSource;
        private bool _isRunning;

        public PeriodicService(INotificationManagerService notificationManagerService, IInvestmentApiClient apiClient)
        {
            this.notificationManagerService = notificationManagerService;
            this.apiClient = apiClient;
            _cancellationTokenSource = new CancellationTokenSource();
        }
        public void StartService()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    await MakeServiceRequest().ConfigureAwait(false);
                    await Task.Delay(2500, _cancellationTokenSource.Token).ConfigureAwait(false);
                }
            }, _cancellationTokenSource.Token);
        }

        public void StopService()
        {
            _cancellationTokenSource.Cancel();
        }

        private async Task MakeServiceRequest()
        {
            try
            {
                var result = await apiClient.GetMarketInfoAsync();
                NotifyUser(result);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void NotifyUser(List<CheckStatusResponse> market)
        {
            foreach (var stock in market) 
            {
                switch (stock.Status)
                {
                    case Status.None:
                        break;
                    case Status.WentUp:
                        notificationManagerService.SendNotification($"{stock.StockIdentifier} Went UP by {stock.PercentValue}% !", "Alert Hit");
                        break;
                    case Status.WentDown:
                        notificationManagerService.SendNotification($"{stock.StockIdentifier} Went Down by {stock.PercentValue}% !", "Alert Hit");
                        break;
                    default:
                        break;
                }

            }
        }
       
    }

}