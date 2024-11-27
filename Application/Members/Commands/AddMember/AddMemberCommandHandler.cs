using Domain.Entities.Members;
using Domain.Repositories;
using MediatR;

namespace Application.Members.Commands.AddMember;

public sealed class AddMemberCommandHandler : IRequestHandler<AddMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AddMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddMemberCommand request, CancellationToken cancellationToken)
    {
        var member = Member.Create(
            request.Username, 
            request.DisplayName);

        await _memberRepository.AddAsync(member, cancellationToken);
        await _unitOfWork.SaveChangeAsync(cancellationToken);
    }
}