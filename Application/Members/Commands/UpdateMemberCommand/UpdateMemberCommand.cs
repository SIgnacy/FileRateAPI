using MediatR;

namespace Application.Members.Commands.UpdateMemberCommand;
public sealed record UpdateMemberCommand(Guid Id, string NewUsername, string NewDisplayName) : IRequest;
