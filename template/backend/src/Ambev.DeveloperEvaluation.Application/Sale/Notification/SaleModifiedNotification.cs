using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.Notification;

public class SaleModifiedNotification : INotification
{
    public Domain.Entities.Sale SaleModified { get; }

    public SaleModifiedNotification(Domain.Entities.Sale saleModified)
    {
        SaleModified = saleModified;
    }
}
