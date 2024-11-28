
using Application.Keywords.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.Keywords;

public sealed class GetKeywords : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/keywords", 
            async 
            (ISender sender, 
            CancellationToken cancellationToken,
            [FromQuery] string? search,
            [FromQuery] string? column,
            [FromQuery] string? order,
            [FromQuery] int page,
            [FromQuery] int size = 10) =>
        {
            GetKeywordsQuery query = new(search, column, order, page, size);
            return Results.Ok(await sender.Send(query, cancellationToken));
        });
    }
}
