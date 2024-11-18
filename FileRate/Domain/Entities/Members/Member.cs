using Domain.Entities.Items;
using Domain.Entities.Ratings;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Members;
public sealed class Member
{
    public MemberId Id { get; set; }
    public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
    public ICollection<Item> Items { get; set; } = new HashSet<Item>();
    public string Username { get; private set; }
    public string DisplayName { get; set; }

    public Member() { }

    public Member(
        MemberId id,
        string username,
        string displayName)
    {
        Id = id;
        Username = username;
        DisplayName = displayName;
    }
}
