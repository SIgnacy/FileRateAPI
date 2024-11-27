using Domain.Common;
using Domain.Entities.Items;
using Domain.Entities.Members;
using Domain.Entities.Ratings;
using Domain.Repositories;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public sealed class MemberRepository : IMemberRepository
{
    private readonly FileRateContext _dbContext;

    public MemberRepository(FileRateContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Member member, 
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<Member>().AddAsync(member, cancellationToken);

    public async Task<PagedResult<Member>> GetAsync(
        string? searchTerm, 
        int page, 
        int pageSize, 
        CancellationToken cancellationToken = default)
    {
        IQueryable<Member> membersQuery = _dbContext.Set<Member>();

        if(!string.IsNullOrWhiteSpace(searchTerm))
        {
            membersQuery = membersQuery.Where(m => 
                m.DisplayName.Contains(searchTerm) ||
                m.Username.Contains(searchTerm)
            );
        }

        var result = await new PagedResult<Member>().Create(
            membersQuery,
            page,
            pageSize,
            cancellationToken);

        return result;
    }

    public async Task<Member?> GetByIdAsync(
        MemberId id, 
        CancellationToken cancellationToken = default) =>
        await _dbContext.Set<Member>().FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

    public Task<PagedResult<Item?>> GetItemsAsync(
        MemberId id, 
        string? sortColumn, 
        string? sortOrder, 
        int page, 
        int pageSize, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResult<Rating?>> GetRatingsAsync(
        MemberId id, 
        string? sortColumn, 
        string? sortOrder, 
        int page, 
        int pageSize, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Remove(Member member) =>
        _dbContext.Set<Member>().Remove(member);

    public void Update(Member member) =>
        _dbContext.Set<Member>().Update(member);
}
