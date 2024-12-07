using Application.Members.Commands.AddMember;
using Domain.Entities.Members;
using Domain.Repositories;
using Moq;

namespace Application.UnitTests.Members.Commands;
public class AddMemberCommandTests
{
    private readonly AddMemberCommand _command = new("test-username", "test-displayname");
    private readonly AddMemberCommandHandler _handler;
    private readonly Mock<IMemberRepository> _memberRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public AddMemberCommandTests()
    {
        _memberRepositoryMock = new Mock<IMemberRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new(_memberRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handler_Should_CallAddAndSaveChanges()
    {
        // Act
        await _handler.Handle(_command, default);

        // Assert
        Assert.Multiple(() =>
        {
            _memberRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Member>(), It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveChangeAsync(It.IsAny<CancellationToken>()), Times.Once);
        });
    }
}
