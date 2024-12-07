using Application.Members.Commands.RemoveMemberCommand;
using Domain.Entities.Members;
using Domain.Exceptions.NotFoundException;
using Domain.Repositories;
using Moq;

namespace Application.UnitTests.Members.Commands;
public class RemoveMemberCommandHandlerTests
{
    private readonly RemoveMemberCommand _command = new(Guid.NewGuid());
    private readonly RemoveMemberCommandHandler _handler;
    private readonly Mock<IMemberRepository> _memberRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public RemoveMemberCommandHandlerTests()
    {
        _memberRepositoryMock = new Mock<IMemberRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new(_memberRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handler_Should_ThrowMemberNotFoundException_WhenMemberNotFound()
    {
        // Arrange
        _memberRepositoryMock.Setup(r => r.GetByIdAsync(
            It.IsAny<MemberId>(),
            It.IsAny<CancellationToken>()
        )).Returns(Task.FromResult<Member?>(null));

        // Act & Assert
        await Assert.ThrowsAsync<MemberNotFoundException>(async () => await _handler.Handle(_command, default));
    }

    [Fact]
    public async Task Handler_Should_CallRemoveAndSaveChanges_WhenMemberIsFound()
    {
        // Arrange
        _memberRepositoryMock.Setup(r => r.GetByIdAsync(
            It.IsAny<MemberId>(),
            It.IsAny<CancellationToken>()
        )).Returns(Task.FromResult<Member?>(new Member()));

        // Act
        await _handler.Handle(_command, default);

        // Assert
        Assert.Multiple(() =>
        {
            _memberRepositoryMock.Verify(r => r.Remove(It.IsAny<Member>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveChangeAsync(It.IsAny<CancellationToken>()), Times.Once);
        });
    }
}
