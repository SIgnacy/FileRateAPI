using Domain.Common;
using Domain.Entities.Items;
using Domain.Repositories;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories;
public sealed class KeywordRepository : IKeywordRepository
{
    private readonly FileRateContext _fileRateContext;

    public KeywordRepository(FileRateContext fileRateContext)
    {
        _fileRateContext = fileRateContext;
    }

    public async Task AddAsync(
        Keyword keyword, 
        CancellationToken cancellationToken = default) 
        => await _fileRateContext.Set<Keyword>().AddAsync(keyword, cancellationToken);

    public async Task<PagedResult<Keyword>> GetAsync(
        string? searchTerm, 
        string? sortColumn, 
        string? sortOrder, 
        int page, 
        int pageSize, 
        CancellationToken cancellationToken = default)
    {
        IQueryable<Keyword> query = _fileRateContext.Set<Keyword>();

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


    public void Remove(Keyword keyword) => _fileRateContext.Set<Keyword>().Remove(keyword);

    public void Update(Keyword keyword) => _fileRateContext.Set<Keyword>().Update(keyword);

    private Expression<Func<Keyword, object>> GetSortProperty(string? sortColumn) =>
        sortColumn?.ToLower() switch
        {
            "popularity" => k => k.Items.Count, 
            "keyword" => k => k.Word,
            _ => k => k.Id
        };
}
