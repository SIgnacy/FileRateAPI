
using Application.Items.Commands.AddItem;
using Domain.Entities.Items;
using Domain.Entities.Members;
using Domain.Exceptions.NotFoundException;
using Domain.Repositories;
using Moq;

namespace Application.UnitTests.Items.Commands;
public class AddItemCommandTests
{
    private readonly AddItemCommand _command = new(new MemberId(
        Guid.NewGuid()),
        [0x01, 0x02, 0x03, 0x04],
        "name",
        "description",
        []);

    private readonly AddItemCommandHandler _handler;

    private readonly Mock<IItemRepository> _itemRepositoryMock;
    private readonly Mock<IMemberRepository> _memberRepositoryMock;
    private readonly Mock<IKeywordRepository> _keywordRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public AddItemCommandTests()
    {
        _itemRepositoryMock = new Mock<IItemRepository>();
        _memberRepositoryMock = new Mock<IMemberRepository>();
        _keywordRepositoryMock = new Mock<IKeywordRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new AddItemCommandHandler(
            _itemRepositoryMock.Object, 
            _memberRepositoryMock.Object, 
            _keywordRepositoryMock.Object, 
            _unitOfWorkMock.Object);
    }

    #region tests

    [Fact]
    public async Task Handler_Should_CallAddAndSaveChanges()
    {
        // Arrange
        _memberRepositoryMock.Setup(m => m.GetByIdAsync(
            It.IsAny<MemberId>(),
            It.IsAny<CancellationToken>()
        )).Returns(Task.FromResult<Member?>(new Member()));

        _keywordRepositoryMock.Setup(k => k.GetByIdAsync(
            It.IsAny<KeywordId>(),
            It.IsAny<CancellationToken>()
        )).Returns(Task.FromResult<Keyword?>(new Keyword()));

        // Act
        await _handler.Handle(_command, default);

        // Assert
        Assert.Multiple(() =>
        {
            _itemRepositoryMock.Verify(i => i.AddAsync(It.IsAny<Item>(), It.IsAny<CancellationToken>()));
            _unitOfWorkMock.Verify(u => u.SaveChangeAsync(It.IsAny<CancellationToken>()));
        });
    }

    [Fact]
    public async Task Handler_Should_CallGetMemberAsync_WhenMemberIsFound()
    {
        // Arrange
        _memberRepositoryMock.Setup(m => m.GetByIdAsync(
            It.IsAny<MemberId>(),
            It.IsAny<CancellationToken>()
        )).Returns(Task.FromResult<Member?>(new Member()));

        _keywordRepositoryMock.Setup(k => k.GetByIdAsync(
            It.IsAny<KeywordId>(), 
            It.IsAny<CancellationToken>()
        )).Returns(Task.FromResult<Keyword?>(new Keyword()));

        // Act
        await _handler.Handle(_command, default);

        // Assert
        _memberRepositoryMock.Verify(m => m.GetByIdAsync(It.IsAny<MemberId>(), It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task Handler_Should_ThrowMemberNotFoundException_WhenMemberIsNotFound()
    {
        // Arrange
        _keywordRepositoryMock.Setup(k => k.GetByIdAsync(
            It.IsAny<KeywordId>(),
            It.IsAny<CancellationToken>()
        )).Returns(Task.FromResult<Keyword?>(new Keyword()));

        // Act & Assert
        await Assert.ThrowsAsync<MemberNotFoundException>(async () => await _handler.Handle(_command, default));
    }

    #endregion
}
