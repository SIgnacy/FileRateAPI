using Application.Keywords.Queries.Responses;
using Domain.Common;
using MediatR;

namespace Application.Keywords.Queries.GetKeywordsQuery;
public sealed record GetKeywordsQuery(string? SearchTerm, string? SortColumn, string? SortOrder, int Page, int PageSize) : IRequest<PagedResult<KeywordResponse>>;
