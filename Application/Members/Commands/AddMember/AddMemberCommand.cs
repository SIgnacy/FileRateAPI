using MediatR;

namespace Application.Members.Commands.AddMember;
public sealed record AddMemberCommand(string Username, string DisplayName) : IRequest;