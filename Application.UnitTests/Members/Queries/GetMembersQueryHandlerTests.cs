using Application.Members.Queries.GetMembers;
using Domain.Common;
using Domain.Entities.Members;
using Domain.Repositories;
using Moq;

namespace Application.UnitTests.Members.Queries;
public class GetMembersQueryHandlerTests
{
    private readonly GetMemberQuery _query = new("", 1, 10);
    private readonly GetMemberQueryHandler _handler;
    private readonly Mock<IMemberRepository> _memberRepository;

    public GetMembersQueryHandlerTests()
    {
         _memberRepository = new Mock<IMemberRepository>();
        _handler = new(_memberRepository.Object);
    }

    [Fact]
    public async Task Handler_Should_CallGetAsync()
    {
        // Arrange
        _memberRepository.Setup(r => r.GetAsync(
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<CancellationToken>()))
        .Returns(Task.FromResult(new PagedResult<Member>() { }));

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        _memberRepository.Verify(r => r.GetAsync(
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<CancellationToken>()), Times.Once);
    }

}
