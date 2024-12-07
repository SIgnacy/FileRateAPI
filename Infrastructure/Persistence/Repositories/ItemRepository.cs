using Domain.Common;
using Domain.Entities.Items;
using Domain.Entities.Members;
using Domain.Entities.Ratings;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public sealed class ItemRepository : IItemRepository
{
    private readonly FileRateContext _fileRateContext;

    public ItemRepository(FileRateContext fileRateContext)
    {
        _fileRateContext = fileRateContext;
    }

    public async Task AddAsync(
        Item item, 
        CancellationToken cancellationToken = default) =>
        await _fileRateContext.Set<Item>().AddAsync(item, cancellationToken);

    public Task<PagedResult<Item>> GetAsync(IEnumerable<string>? keywords, string? searchTErm, string? sortColumn, string? sortOrder, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Item?> GetByIdAsync(ItemId id, CancellationToken cancellationToken = default) =>
        await _fileRateContext.Set<Item>().FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

    public Task<PagedResult<Rating>> GetRatingsAsync(ItemId id, string? sortColumn, string? sortOrder, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task Rate(ItemId itemId, MemberId memberId, int rate, string comment, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Remove(Item item) =>
        _fileRateContext.Set<Item>().Remove(item);

    public void Update(Item item) =>
        _fileRateContext.Set<Item>().Update(item);
}
