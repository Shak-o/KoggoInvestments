using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoggoInvestments.Ui.Notifications
{
    public interface INotificationManagerService
    {
        event EventHandler NotificationReceived;
        void SendNotification(string title, string message);
        void ReceiveNotification(string title, string message);
    }
}
