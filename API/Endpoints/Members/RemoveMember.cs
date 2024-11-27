using API.Endpoints;
using Application.Members.Commands.RemoveMemberCommand;
using MediatR;

namespace API.Endpoints.Members;

public sealed class RemoveMember : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/members/{id}",
            async
            (ISender sender,
            CancellationToken cancellationToken,
            Guid id) =>
        {
            RemoveMemberCommand command = new(id);
            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        });
    }
}
