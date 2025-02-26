using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM;

public class UnitOfWork : IUnitOfWork
{
    private readonly DefaultContext _dbContext;

    public UnitOfWork(DefaultContext dbCcontext)
    {
        _dbContext = dbCcontext;
    }

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}

