using Application.Keywords.Queries.GetKeywordsQuery;
using Domain.Common;
using Domain.Entities.Items;
using Domain.Repositories;
using Moq;

namespace Application.UnitTests.Keywords.Queries;

public class GetKeywordsQueryHandlerTests
{
    private readonly GetKeywordsQuery _query = new("", "", "", 1, 10);
    private readonly GetKeywordsQueryHandler _handler;
    private readonly Mock<IKeywordRepository> _repositoryMock;

    public GetKeywordsQueryHandlerTests()
    {
        _repositoryMock = new Mock<IKeywordRepository>();
        _handler = new GetKeywordsQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_CallGetAsync()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<CancellationToken>()
        )).Returns(
            Task.FromResult(new PagedResult<Keyword> { }));

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        _repositoryMock.Verify(r => r.GetAsync(
            It.IsAny<string>(), 
            It.IsAny<string>(), 
            It.IsAny<string>(), 
            It.IsAny<int>(), 
            It.IsAny<int>(), 
            It.IsAny<CancellationToken>()), 
        Times.Once);
    }
}
