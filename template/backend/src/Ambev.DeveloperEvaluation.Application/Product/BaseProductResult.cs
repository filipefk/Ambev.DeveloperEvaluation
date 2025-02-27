namespace Ambev.DeveloperEvaluation.Application.Product;

public class BaseProductResult
{
    public required Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public required BaseRatingApp Rating { get; set; }
}
