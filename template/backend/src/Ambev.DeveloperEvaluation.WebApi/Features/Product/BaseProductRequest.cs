namespace Ambev.DeveloperEvaluation.WebApi.Features.Product;

public class BaseProductRequest
{
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public required BaseRatingApi Rating { get; set; }
}
