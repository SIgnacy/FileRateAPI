using Application.Members.Queries.Responses;
using Domain.Common;
using Domain.Entities.Members;

namespace Application.Mappers;
public static class PagedResultMapper
{
    public static PagedResult<MemberResponse> ToMemberResponse(this PagedResult<Member> result) =>
    new PagedResult<MemberResponse>
    {
        Items = result.Items?.Select(member => new MemberResponse(
            member.Id.Value,
            member.Username,
            member.DisplayName
        )).ToList(),
        Page = result.Page,
        Size = result.Size,
        TotalCount = result.TotalCount
    };
}
