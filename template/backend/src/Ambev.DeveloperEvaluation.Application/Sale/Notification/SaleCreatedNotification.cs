using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.Notification;

public class SaleCreatedNotification : INotification
{
    public Domain.Entities.Sale SaleCreated { get; }

    public SaleCreatedNotification(Domain.Entities.Sale sale)
    {
        SaleCreated = sale;
    }
}
