using Domain.Entities.Members;
using Domain.Exceptions.NotFoundException;
using Domain.Repositories;
using MediatR;

namespace Application.Members.Commands.RemoveMemberCommand;

public sealed class RemoveMemberCommandHandler : IRequestHandler<RemoveMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(new MemberId(request.Id), cancellationToken) 
            ?? throw new MemberNotFoundException($"Member with id={request.Id} Not Found.");

        _memberRepository.Remove(member);
        await _unitOfWork.SaveChangeAsync(cancellationToken);
    }
}
