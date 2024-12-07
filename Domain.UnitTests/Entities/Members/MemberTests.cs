using Domain.Entities.Members;

namespace Domain.UnitTests.Entities.Members;

public class MemberTests
{
    #region Create Tests

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_Should_ThrowArgumentException_WhenUsernameIsNullOrWhiteSpace(string username)
    {
        // Arrange
        string displayname = "test";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Member.Create(username, displayname));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_Should_ThrowArgumentException_WhenDisplayNameIsNullOrWhiteSpace(string displayname)
    {
        // Arrange
        string username = "test";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Member.Create(username, displayname));
    }

    [Fact]
    public void Create_Should_CreateUniqueId_WhenUserIsCreated()
    {
        // Arrange
        string UsernameU1 = "usernameU1";
        string DisplaynameU1 = "displaynameU1";

        string UsernameU2 = "usernameU2";
        string DisplaynameU2 = "displaynameU2";

        // Act
        var member1 = Member.Create(UsernameU1, DisplaynameU1);
        var member2 = Member.Create(UsernameU2, DisplaynameU2);

        // Assert
        Assert.NotEqual(member1.Id, member2.Id);
    }

    [Fact] 
    public void Create_Should_CreateMember_WithCorrectlyAssignedValues()
    {
        // Arrange
        string username = "test-username";
        string displayname = "test-displayname";

        // Act
        var member = Member.Create(username, displayname);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(member.Username, username),
            () => Assert.Equal(member.DisplayName, displayname));
    }

    #endregion

    #region Update Tests

    [Theory]
    [InlineData("", null)]
    [InlineData(null, "")]
    [InlineData("   ", "        ")]
    public void Update_ShouldNotUpdate_WhenNewValuesAreNullOrWhiteSpace(string newUsername, string newDisplayname)
    {
        // Arrange
        string username = "test-username";
        string displayname = "test-displayname";
        var member = Member.Create(username, displayname);

        // Act
        member.Update(newUsername, newDisplayname);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(member.Username, username),
            () => Assert.Equal(member.DisplayName, displayname));
    }

    [Fact]
    public void Update_Should_UpdateMember_WithCorrectlyAssignedValues()
    {
        // Arrange
        string oldUsername = "old-un";
        string oldDisplayname = "old-dn";

        string newUsername = "new-un";
        string newDisplayname = "new-dn";

        var member = Member.Create(oldUsername, oldDisplayname);
        var id = member.Id;

        // Act
        member.Update(newUsername, newDisplayname);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(member.Id, id),
            () => Assert.Equal(member.Username, newUsername),
            () => Assert.Equal(member.DisplayName, newDisplayname));
    }

    #endregion
}
