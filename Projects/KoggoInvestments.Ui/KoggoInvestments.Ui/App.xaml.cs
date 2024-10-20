using KoggoInvestments.Ui.Notifications;
using KoggoInvestments.Ui.Services;
using KoggoInvestments.UI.Services;

namespace KoggoInvestments.Ui
{
    public partial class App : Application
    {
        private PeriodicService _periodicService;
        private readonly INotificationManagerService notificationManagerService;


        public App(INotificationManagerService notificationManagerService, IInvestmentApiClient investmentApiClient)
        {
            InitializeComponent();

            MainPage = new AppShell();
            _periodicService = new PeriodicService(notificationManagerService, investmentApiClient);
        }

        protected override void OnStart()
        {
            _periodicService.StartService();
        }
        protected override void OnSleep()
        {
            _periodicService.StopService();
        }
        protected override void OnResume()
        {
            _periodicService.StartService();
        }
    }
}
