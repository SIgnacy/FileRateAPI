using Application.Members.Queries.GetMemberById;
using Domain.Entities.Members;
using Domain.Exceptions.NotFoundException;
using Domain.Repositories;
using Moq;

namespace Application.UnitTests.Members.Queries;
public class GetMemberByIdQueryHandlerTests
{
    private readonly GetMemberByIdQuery _query = new(Guid.NewGuid());
    private readonly GetMemberByIdQueryHandler _handler;
    private readonly Mock<IMemberRepository> _memberRepositoryMock;

    public GetMemberByIdQueryHandlerTests()
    {
        _memberRepositoryMock = new Mock<IMemberRepository>();
        _handler = new(_memberRepositoryMock.Object);
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
        await Assert.ThrowsAsync<MemberNotFoundException>(async () => await _handler.Handle(_query, default));
    }

    [Fact]
    public async Task Handler_Should_CallGetByIdAsync()
    {
        // Arrange
        _memberRepositoryMock.Setup(r => r.GetByIdAsync(
            It.IsAny<MemberId>(),
            It.IsAny<CancellationToken>()
        )).Returns(Task.FromResult<Member?>(Member.Create("test-username","test-displayname")));

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        _memberRepositoryMock.Verify(r => r.GetByIdAsync(
            It.IsAny<MemberId>(), 
            It.IsAny<CancellationToken>()), 
        Times.Once);
    }
}
