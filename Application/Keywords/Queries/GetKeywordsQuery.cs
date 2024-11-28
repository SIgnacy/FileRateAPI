using Application.Mappers;
using Application.Keywords.Responses;
using Domain.Common;
using Domain.Repositories;
using MediatR;

namespace Application.Keywords.Queries;
public sealed record GetKeywordsQuery(string? SearchTerm, string? SortColumn, string? SortOrder, int Page, int PageSize) : IRequest<PagedResult<KeywordResponse>>;

public sealed class GetKeywordsQueryHandler : IRequestHandler<GetKeywordsQuery, PagedResult<KeywordResponse>>
{
    private readonly IKeywordRepository _keywordRepository;

    public GetKeywordsQueryHandler(IKeywordRepository keywordRepository)
    {
        _keywordRepository = keywordRepository;
    }

    public async Task<PagedResult<KeywordResponse>> Handle(GetKeywordsQuery request, CancellationToken cancellationToken)
    {
        var response = await _keywordRepository.GetAsync(
            request.SearchTerm, 
            request.SortColumn,
            request.SortOrder,
            request.Page,
            request.PageSize,
            cancellationToken
            );

        return response.ToKeywordResponse();
    }
}
