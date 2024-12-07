using Domain.Entities.Items;
using Domain.Entities.Ratings;

namespace Domain.Entities.Members;
public sealed class Member
{
    public MemberId Id { get; private set; }
    public ICollection<Rating> Ratings { get; private set; } = new HashSet<Rating>();
    public ICollection<Item> Items { get; private set; } = new HashSet<Item>();
    public string Username { get; private set; }
    public string DisplayName { get; private set; }

    public Member() { }

    public static Member Create(string username, string displayName)
    {
        if(string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username cannot be empty.");
        if(string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("DisplayName cannot be empty.");

        return new Member
        {
            Id = new MemberId(Guid.NewGuid()),
            Username = username,
            DisplayName = displayName
        };
    }

    public void Update(string? newUsername, string? newDisplayName)
    {
        if (!string.IsNullOrWhiteSpace(newUsername)) Username = newUsername;
        if (!string.IsNullOrWhiteSpace(newDisplayName)) DisplayName = newDisplayName;
    }

    public override bool Equals(object? obj)
    {
        if(obj is null) return false;
        if(obj.GetType() != GetType()) return false;
        if(obj is not Member member) return false;
        return member.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 13;
    }
}
