using Domain.Entities.Members;
using Domain.Entities.Ratings;

namespace Domain.Entities.Items;
public sealed class Item
{
    public ItemId Id { get; set; }
    public MemberId MemberId { get; private set; }
    public Member Member { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public byte[] File { get; private set; }
    public HashSet<Keyword> Keywords { get; private set; } = new HashSet<Keyword>();
    public ICollection<Rating> Rating { get; private set; } = new HashSet<Rating>();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Item() { }

    public Item(
        ItemId id, 
        MemberId memberId, 
        Member member, 
        string name, 
        string description, 
        byte[] file, 
        HashSet<Keyword> keywords, 
        ICollection<Rating> rating, 
        DateTime createdAt, 
        DateTime updatedAt)
    {
        Id = id;
        MemberId = memberId;
        Member = member;
        Name = name;
        Description = description;
        File = file;
        Keywords = keywords;
        Rating = rating;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
