using KoggoInvestments.UI.Services;

namespace KoggoInvestments.Ui
{
    public partial class App : Application
    {
        private PeriodicService _periodicService;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            _periodicService = new PeriodicService();
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
