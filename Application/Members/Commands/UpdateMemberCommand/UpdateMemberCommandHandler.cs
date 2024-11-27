using Domain.Entities.Members;
using Domain.Exceptions.NotFoundException;
using Domain.Repositories;
using MediatR;

namespace Application.Members.Commands.UpdateMemberCommand;

public sealed class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(new MemberId(request.Id), cancellationToken) 
            ?? throw new MemberNotFoundException($"Member with id={request.Id} Not Found.");

        member.Update(request.NewUsername, request.NewDisplayName);

        _memberRepository.Update(member);
        await _unitOfWork.SaveChangeAsync(cancellationToken);
    }
}
