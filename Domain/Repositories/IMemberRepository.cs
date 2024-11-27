using Domain.Common;
using Domain.Entities.Items;
using Domain.Entities.Members;
using Domain.Entities.Ratings;

namespace Domain.Repositories;
public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(MemberId id, CancellationToken cancellationToken = default);
    Task<PagedResult<Member>> GetAsync(string? searchTerm, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<PagedResult<Rating?>> GetRatingsAsync(MemberId id, string? sortColumn, string? sortOrder, int page, int pageSize, CancellationToken cancellationToken = default);         
    Task<PagedResult<Item?>> GetItemsAsync(MemberId id, string? sortColumn, string? sortOrder, int page, int pageSize, CancellationToken cancellationToken = default);             
    Task AddAsync(Member member, CancellationToken cancellationToken = default);
    void Update(Member member);
    void Remove(Member member);
}
