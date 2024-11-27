using Application.Mappers;
using Application.Members.Queries.Responses;
using Domain.Common;
using Domain.Repositories;
using MediatR;

namespace Application.Members.Queries.GetMembers;

public sealed class GetMemberQueryHandler : IRequestHandler<GetMemberQuery, PagedResult<MemberResponse>>
{
    private readonly IMemberRepository _memberRepository;

    public GetMemberQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<PagedResult<MemberResponse>> Handle(GetMemberQuery request, CancellationToken cancellationToken)
    {
        var result = await _memberRepository.GetAsync(request.SearchTerm, request.Page, request.PageSize, cancellationToken);
        return result.ToMemberResponse();
    }
}