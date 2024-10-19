namespace KoggoInvestments.Ui.Models;

public class NotificationEventArgs : EventArgs
{
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
}

