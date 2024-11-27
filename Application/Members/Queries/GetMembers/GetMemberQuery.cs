using Application.Members.Queries.Responses;
using Domain.Common;
using MediatR;

namespace Application.Members.Queries.GetMembers;

public sealed record GetMemberQuery(string? SearchTerm, int Page, int PageSize) : IRequest<PagedResult<MemberResponse>>;
