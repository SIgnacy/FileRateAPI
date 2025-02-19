using API.Endpoints;
using Application.Members.Queries.GetMembers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.Members;

public sealed class GetMembers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/members",
            async
            (ISender sender,
            CancellationToken cancellationToken,
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10) =>
        {
            GetMemberQuery query = new(search, page, size);
            return Results.Ok(await sender.Send(query, cancellationToken));
        }).WithTags("Members");
    }
}