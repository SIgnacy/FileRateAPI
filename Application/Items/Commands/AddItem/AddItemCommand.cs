using Domain.Entities.Items;
using Domain.Entities.Members;
using MediatR;

namespace Application.Items.Commands.AddItem;
public sealed record AddItemCommand(
        MemberId MemberId, 
        byte[] File, 
        string Name, 
        string Description, 
        List<KeywordId> Keywords
    ) : IRequest;
