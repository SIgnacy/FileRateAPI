namespace Domain.Repositories;
public interface IUnitOfWork
{
    public Task SaveChangeAsync(CancellationToken cancellationToken = default);
}
