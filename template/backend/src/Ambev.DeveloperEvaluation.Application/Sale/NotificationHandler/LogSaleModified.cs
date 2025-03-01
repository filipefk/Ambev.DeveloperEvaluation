using Ambev.DeveloperEvaluation.Application.Sale.Notification;
using MediatR;
using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Sale.NotificationHandler;

public class LogSaleModified : INotificationHandler<SaleModifiedNotification>
{
    public Task Handle(SaleModifiedNotification notification, CancellationToken cancellationToken)
    {
        Log.Information("Sale modified: Sale ID = {ID}", notification.SaleModified.Id);

        return Task.CompletedTask;
    }
}
