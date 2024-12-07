using Application.Mappers;
using Domain.Common;
using Domain.Repositories;
using MediatR;
using Application.Keywords.Queries.Responses;

namespace Application.Keywords.Queries.GetKeywordsQuery;

internal sealed class GetKeywordsQueryHandler : IRequestHandler<GetKeywordsQuery, PagedResult<KeywordResponse>>
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
