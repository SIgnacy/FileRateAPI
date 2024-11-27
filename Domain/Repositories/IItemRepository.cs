using Domain.Common;
using Domain.Entities.Items;
using Domain.Entities.Ratings;

namespace Domain.Repositories;

public interface IItemRepository
{
    Task<Item?> GetByIdAsync(ItemId id);
    Task<PagedResult<Item?>> GetAsync(IEnumerable<Keyword>? keywords, string? sortColumn, string? sortOrder, int page, int pageSize);
    Task<PagedResult<Rating?>> GetRatingsAsync(ItemId id, string? sortColumn, string? sortOrder, int page, int pageSize);
    Task AddAsync(Item item);
    void Update(Item item); 
    void Remove(Item item);
}
