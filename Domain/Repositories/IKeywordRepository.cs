using Domain.Common;
using Domain.Entities.Items;

namespace Domain.Repositories;

public interface IKeywordRepository
{
    Task<PagedResult<Keyword>> GetAsync(string? searchTerm, string? sortColumn, string? sortOrder, int page, int pageSize, CancellationToken cancellationToken = default);
    Task AddAsync(Keyword keyword, CancellationToken cancellationToken = default);
    void Update(Keyword keyword);
    void Remove(Keyword keyword);
}