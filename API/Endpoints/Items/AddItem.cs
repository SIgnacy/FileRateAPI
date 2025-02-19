
using API.Extensions;
using Application.Items.Commands.AddItem;
using Domain.Entities.Items;
using Domain.Entities.Members;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints.Items;

public sealed class AddItem : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/items",
            async (ISender sender,
            CancellationToken cancellationToken,
            IFormFile file,
            [FromForm] Guid memberId,
            [FromForm] string name,
            [FromForm] string description,
            [FromForm] List <KeywordId> keywords) =>
            {
                AddItemCommand command = new(
                    new MemberId(memberId),
                    await file.ToByteArrayAsync(),
                    name,
                    description,
                    keywords);
                
                await sender.Send(command, cancellationToken);
                return Results.NoContent;
            }).WithTags("Items");
    }
}
