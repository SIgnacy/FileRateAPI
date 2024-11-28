using Application.Keywords.Responses;
using Application.Members.Queries.Responses;
using Domain.Common;
using Domain.Entities.Items;
using Domain.Entities.Members;

namespace Application.Mappers;
public static class PagedResultMapper
{
    public static PagedResult<MemberResponse> ToMemberResponse(this PagedResult<Member> result) =>
        new PagedResult<MemberResponse>
        {
            Items = result.Items?.Select(m => new MemberResponse(
                m.Id.Value,
                m.Username,
                m.DisplayName
            )).ToList(),
            Page = result.Page,
            Size = result.Size,
            TotalCount = result.TotalCount
        };

    public static PagedResult<KeywordResponse> ToKeywordResponse(this PagedResult<Keyword> result) =>
        new PagedResult<KeywordResponse>
        {
            Items = result.Items?.Select(k => new KeywordResponse(
                k.Id.Value, 
                k.Word
            )).ToList(),
            Page = result.Page,
            Size = result.Size,
            TotalCount = result.TotalCount
        };
}
