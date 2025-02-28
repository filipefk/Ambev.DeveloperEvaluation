using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        return sale;
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Sales
            .Include(sale => sale.Products)
                .ThenInclude(saleProduct => saleProduct.Product)
            .Include(sale => sale.Discounts)
            .FirstOrDefaultAsync(sale => sale.Id == id, cancellationToken);
    }

    public async Task<Sale?> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context
            .Sales
            .Include(sale => sale.Products)
                .ThenInclude(saleProduct => saleProduct.Product)
            .Include(sale => sale.Discounts)
            .FirstOrDefaultAsync(sale => sale.UserId == userId, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale == null)
            return false;

        _context.Sales.Remove(sale);

        return true;
    }

    public async Task<PaginatedResult<Sale>?> GetAllAsync(int currentPage, int pageSize, string order = null!, CancellationToken cancellationToken = default)
    {
        var totalCount = await _context.Sales.CountAsync(cancellationToken);

        if (totalCount == 0)
            return null;

        var query = _context
            .Sales
            .AsNoTracking()
            .Include(sale => sale.Products)
                .ThenInclude(saleProduct => saleProduct.Product)
            .Include(sale => sale.Discounts)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(order))
            query = QueryableOrderer.ApplyOrdering(query, order);

        var sales = await query
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var results = new PaginatedResult<Sale>(sales, currentPage, pageSize, totalCount);

        return results;
    }
}
