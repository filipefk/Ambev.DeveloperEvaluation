using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly DefaultContext _context;

    public BranchRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        await _context.Branches.AddAsync(branch, cancellationToken);
        return branch;
    }

    public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Branches
            .FirstOrDefaultAsync(branch => branch.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var branch = await GetByIdAsync(id, cancellationToken);
        if (branch == null)
            return false;

        _context.Branches.Remove(branch);

        return true;
    }

    public async Task<PaginatedResult<Branch>?> GetAllAsync(int currentPage, int pageSize, string order = null!, CancellationToken cancellationToken = default)
    {
        var totalCount = await _context.Branches.CountAsync(cancellationToken);

        if (totalCount == 0)
            return null;

        var query = _context
            .Branches
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(order))
            query = QueryableOrderer.ApplyOrdering(query, order);

        var branchs = await query
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var results = new PaginatedResult<Branch>(branchs, currentPage, pageSize, totalCount);

        return results;
    }

}
