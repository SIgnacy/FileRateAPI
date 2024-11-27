using Application.Members.Queries.Responses;
using Domain.Entities.Members;

namespace Application.Mappers;
public static class MemberMapper
{
    public static MemberResponse ToMemberResponse(this Member member)
    {
        return new MemberResponse(member.Id.Value, member.Username, member.DisplayName);
    }
}
