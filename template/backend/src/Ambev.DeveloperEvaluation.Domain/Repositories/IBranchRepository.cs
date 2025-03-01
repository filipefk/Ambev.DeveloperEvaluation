using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IBranchRepository
{
    Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default);

    Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PaginatedResult<Branch>?> GetAllAsync(int currentPage, int pageSize, string order = null!, CancellationToken cancellationToken = default);
}
