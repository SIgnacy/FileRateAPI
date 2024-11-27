using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Extensions;
internal static class PagedResultExtensions
{
    public static async Task<PagedResult<T>> Create<T>(
        this PagedResult<T> result,
        IQueryable<T> query, 
        int page, 
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        ValidateArguments(page, pageSize);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        result.Items = items;
        result.Page = page;
        result.Size = pageSize;
        result.TotalCount = totalCount;
        return result;
    }

    private static void ValidateArguments(int page, int pageSize)
    {
        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page), "Page must be greater or equal to 1.");
        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "PageSize must be greater or equal to 1.");
    }
}
