using Application.Members.Queries.Responses;
using MediatR;

namespace Application.Members.Queries.GetMemberById;
public sealed record GetMemberByIdQuery(Guid Id) : IRequest<MemberResponse>;
