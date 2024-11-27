using MediatR;

namespace Application.Members.Commands.RemoveMemberCommand;
public sealed record RemoveMemberCommand(Guid Id) : IRequest;
