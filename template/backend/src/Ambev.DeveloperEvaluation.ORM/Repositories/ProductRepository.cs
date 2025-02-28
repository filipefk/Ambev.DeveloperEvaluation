using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        return product;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Products
            .Include(product => product.Rating)
            .FirstOrDefaultAsync(product => product.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        _context.Products.Remove(product);

        return true;
    }

    public async Task<PaginatedResult<Product>?> GetAllAsync(int currentPage, int pageSize, string order = null!, CancellationToken cancellationToken = default)
    {
        var totalCount = await _context.Products.CountAsync(cancellationToken);

        if (totalCount == 0)
            return null;

        var query = _context
            .Products
            .Include(product => product.Rating)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(order))
            query = QueryableOrderer.ApplyOrdering(query, order);

        var products = await query
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var results = new PaginatedResult<Product>(products, currentPage, pageSize, totalCount);

        return results;
    }

}
