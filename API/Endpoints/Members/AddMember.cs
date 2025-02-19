using API.Endpoints;
using Application.Members.Commands.AddMember;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.Members;

public sealed class AddMember : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/members",
            async
            (ISender sender,
            CancellationToken cancellationToken,
            AddMemberCommand command) =>
        {
            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        }).WithTags("Members");
    }
}
