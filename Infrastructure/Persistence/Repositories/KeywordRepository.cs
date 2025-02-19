using Domain.Common;
using Domain.Entities.Items;
using Domain.Repositories;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories;
public sealed class KeywordRepository : IKeywordRepository
{
    private readonly FileRateContext _dbContext;

    public KeywordRepository(FileRateContext fileRateContext)
    {
        _dbContext = fileRateContext;
    }

    public async Task AddAsync(
        Keyword keyword,
        CancellationToken cancellationToken = default) =>
            await _dbContext.Set<Keyword>().AddAsync(keyword, cancellationToken);

    public async Task AddMultipleAsync(
        IEnumerable<Keyword> keywords, 
        CancellationToken cancellationToken = default) =>
            await _dbContext.Set<Keyword>().AddRangeAsync(keywords, cancellationToken);

    public async Task<IEnumerable<bool>> CheckKeywordsAsync(IEnumerable<string> keywords, CancellationToken cancellationToken = default)
    { 
        var tasks = keywords.Select(async w => 
            await _dbContext.Set<Keyword>().AnyAsync(k => k.Word == w)).ToList();

        return await Task.WhenAll(tasks);
    }

    public async Task<PagedResult<Keyword>> GetAsync(
        string? searchTerm, 
        string? sortColumn, 
        string? sortOrder, 
        int page, 
        int pageSize, 
        CancellationToken cancellationToken = default)
    {
        IQueryable<Keyword> query = _dbContext.Set<Keyword>();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(k =>
                k.Word.Contains(searchTerm));
        }

        if (sortColumn?.ToLower() == "popularity") 
            sortOrder = sortOrder?.ToLower() == "desc" ? "" : "desc";

        query = sortOrder?.ToLower() == "desc"
            ? query.OrderByDescending(GetSortProperty(sortColumn))
            : query.OrderBy(GetSortProperty(sortColumn));

        var result = await new PagedResult<Keyword>().Create(
            query,
            page,
            pageSize,
            cancellationToken);

        return result;
    }

    public async Task<Keyword?> GetByIdAsync(
        KeywordId id, 
        CancellationToken cancellationToken = default) => 
        await _dbContext.Set<Keyword>().FirstOrDefaultAsync(k => k.Id == id, cancellationToken);

    public void Remove(Keyword keyword) => _dbContext.Set<Keyword>().Remove(keyword);

    public void Update(Keyword keyword) => _dbContext.Set<Keyword>().Update(keyword);

    private Expression<Func<Keyword, object>> GetSortProperty(string? sortColumn) =>
        sortColumn?.ToLower() switch
        {
            "popularity" => k => k.Items.Count, 
            "keyword" => k => k.Word,
            _ => k => k.Id
        };
}
