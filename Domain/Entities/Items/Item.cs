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

    public static Item Create(
        MemberId memberId, 
        Member member, 
        byte[] file, 
        string name, 
        string description, 
        HashSet<Keyword> keywords)
    {
        return new Item 
        {
            Id = new ItemId(Guid.NewGuid()),
            MemberId = memberId,
            Member = member,
            File = file,
            Name = name,
            Description = description,
            Keywords = keywords,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
}
