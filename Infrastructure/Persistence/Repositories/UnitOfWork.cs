using Domain.Repositories;

namespace Infrastructure.Persistence.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly FileRateContext _context;

    public UnitOfWork(FileRateContext context)
    {
        _context = context;
    }

    public Task SaveChangeAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);
}
