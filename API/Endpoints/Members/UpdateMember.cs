using API.Endpoints;
using Application.Members.Commands.UpdateMemberCommand;
using MediatR;

namespace API.Endpoints.Members;

public sealed class UpdateMember : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("api/members",
            async
            (ISender sender,
            CancellationToken cancellationToken,
            UpdateMemberCommand command) =>
        {
            await sender.Send(command, cancellationToken);
        });
    }
}
