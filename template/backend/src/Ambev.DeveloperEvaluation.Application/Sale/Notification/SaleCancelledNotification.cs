using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.Notification;

public class SaleCancelledNotification : INotification
{
    public Domain.Entities.Sale SaleCanceled { get; }

    public SaleCancelledNotification(Domain.Entities.Sale sale)
    {
        SaleCanceled = sale;
    }
}
