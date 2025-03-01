using Ambev.DeveloperEvaluation.Application.Sale.Notification;
using MediatR;
using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Sale.NotificationHandler;

public class LogSaleCancelled : INotificationHandler<SaleCancelledNotification>
{
    public Task Handle(SaleCancelledNotification notification, CancellationToken cancellationToken)
    {
        Log.Information("Sale canceled: Sale ID = {ID}", notification.SaleCanceled.Id);

        return Task.CompletedTask;
    }
}
