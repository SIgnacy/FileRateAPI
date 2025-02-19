using API.Endpoints;
using Application.Members.Queries.GetMemberById;
using MediatR;

namespace API.Endpoints.Members;

public sealed class GetMemberById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/members/{id}",
            async
            (ISender sender,
            CancellationToken cancellationToken,
            Guid id) =>
        {
            GetMemberByIdQuery query = new(id);
            return Results.Ok(await sender.Send(query, cancellationToken));
        }).WithTags("Members"); ;
    }
}
