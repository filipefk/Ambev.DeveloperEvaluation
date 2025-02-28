namespace Ambev.DeveloperEvaluation.Application.Cart;

public class BaseCartCommand
{
    public Guid UserId { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public ICollection<BaseCartProductApp> Products { get; set; } = [];
}
