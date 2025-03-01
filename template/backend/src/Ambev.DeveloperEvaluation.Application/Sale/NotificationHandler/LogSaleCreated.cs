using Ambev.DeveloperEvaluation.Application.Sale.Notification;
using MediatR;
using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Sale.NotificationHandler;

public class LogSaleCreated : INotificationHandler<SaleCreatedNotification>
{
    public Task Handle(SaleCreatedNotification notification, CancellationToken cancellationToken)
    {
        Log.Information("Sale created: Sale ID = {ID}", notification.SaleCreated.Id);

        return Task.CompletedTask;
    }
}
