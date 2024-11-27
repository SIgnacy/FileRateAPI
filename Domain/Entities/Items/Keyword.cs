
namespace Domain.Entities.Items;
public sealed class Keyword
{
    public KeywordId Id { get; set; }
    public string Word { get; private set; }
    public HashSet<Item> Items { get; private set; } = new HashSet<Item>();

    public Keyword() { }

    public Keyword(
        KeywordId id, 
        string word, 
        HashSet<Item> items)
    {
        Id = id;
        Word = word;
        Items = items;
    }
}
