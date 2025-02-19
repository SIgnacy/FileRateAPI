using Domain.Entities.Items;
using Domain.Entities.Members;
using Domain.Exceptions.NotFoundException;
using Domain.Repositories;
using MediatR;

namespace Application.Items.Commands.AddItem;

internal sealed class AddItemCommandHandler : IRequestHandler<AddItemCommand>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IKeywordRepository _keywordRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddItemCommandHandler(
        IItemRepository itemRepository, 
        IMemberRepository memberRepository, 
        IKeywordRepository keywordRepository, 
        IUnitOfWork unitOfWork)
    {
        _itemRepository = itemRepository;
        _memberRepository = memberRepository;
        _keywordRepository = keywordRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        Member member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken) 
            ?? throw new MemberNotFoundException($"Member with id={request.MemberId} Not Found.");

        HashSet<Keyword> keywords = new HashSet<Keyword>(( 
            await Task
            .WhenAll(request.Keywords
            .Select(id => _keywordRepository.GetByIdAsync(id, cancellationToken))))
            .Where(k => k != null)!);

        Item item = Item.Create(
            request.MemberId,
            member,
            request.File,
            request.Name,
            request.Description,
            keywords
            );

        await _itemRepository.AddAsync(item, cancellationToken);
        await _unitOfWork.SaveChangeAsync(cancellationToken);
    }
}