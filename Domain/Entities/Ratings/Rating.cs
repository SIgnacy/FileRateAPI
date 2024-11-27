using Domain.Entities.Items;
using Domain.Entities.Members;

namespace Domain.Entities.Ratings;
public sealed class Rating
{
    public RatingId Id { get; set; }
    public ItemId ItemId { get; private set; }
    public Item Item { get; private set; }
    public MemberId MemberId { get; private set; }
    public Member Member { get; private set; }
    public int Rate { get; private set; }
    public string Comment { get; private set; }

    public Rating() { }

    public Rating(
        RatingId id, 
        ItemId itemId, 
        Item item, 
        MemberId memberId, 
        Member member, 
        int rate, 
        string comment)
    {
        Id = id;
        ItemId = itemId;
        Item = item;
        MemberId = memberId;
        Member = member;
        Rate = rate;
        Comment = comment;
    }
}
