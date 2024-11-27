using Domain.Common;
using Domain.Entities.Items;

namespace Domain.Repositories;

public interface IKeywordRepository
{
    Task<PagedResult<Keyword?>> GetAsync(string? searchTerm, string? sortColumn, string? sortOrder, int page, int pageSize);
    Task AddAsync(Keyword keyword);
    Task RemoveAsync(Keyword keyword);
}