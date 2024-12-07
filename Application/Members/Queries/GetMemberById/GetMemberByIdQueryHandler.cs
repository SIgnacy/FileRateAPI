using Application.Mappers;
using Application.Members.Queries.Responses;
using Domain.Entities.Members;
using Domain.Exceptions.NotFoundException;
using Domain.Repositories;
using MediatR;

namespace Application.Members.Queries.GetMemberById;

internal sealed class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, MemberResponse>
{
    private readonly IMemberRepository _memberRepository;
    public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<MemberResponse> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(new MemberId(request.Id), cancellationToken) 
            ?? throw new MemberNotFoundException($"Member with id={request.Id} Not Found.");

        return member.ToMemberResponse();
    }
}