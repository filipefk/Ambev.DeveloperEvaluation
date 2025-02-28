using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CartRepository : ICartRepository
{
    private readonly DefaultContext _context;

    public CartRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await _context.Carts.AddAsync(cart, cancellationToken);
        return cart;
    }

    public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Carts
            .Include(cart => cart.Products)
                .ThenInclude(cartProduct => cartProduct.Product)
            .FirstOrDefaultAsync(cart => cart.Id == id, cancellationToken);
    }

    public async Task<Cart?> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context
            .Carts
            .Include(cart => cart.Products)
                .ThenInclude(cartProduct => cartProduct.Product)
            .FirstOrDefaultAsync(cart => cart.UserId == userId, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(id, cancellationToken);
        if (cart == null)
            return false;

        _context.Carts.Remove(cart);

        return true;
    }

    public async Task<PaginatedResult<Cart>?> GetAllAsync(int currentPage, int pageSize, string order = null!, CancellationToken cancellationToken = default)
    {
        var totalCount = await _context.Carts.CountAsync(cancellationToken);

        if (totalCount == 0)
            return null;

        var query = _context
            .Carts
            .AsNoTracking()
            .Include(cart => cart.Products)
                .ThenInclude(cartProduct => cartProduct.Product)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(order))
            query = QueryableOrderer.ApplyOrdering(query, order);

        var carts = await query
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var results = new PaginatedResult<Cart>(carts, currentPage, pageSize, totalCount);

        return results;
    }

}
