using RPGBot.Database;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace RPGBot.Data;

public class Items
{
    private static Dictionary<int, Item> _items;
    private static readonly List<int> _presentIds = [201, 202, 203, 204];
    public static Dictionary<int, Item> GetItems()
    {
        _items ??= new Dictionary<int, Item>()
        {
            [201] = new Weapon
            {
                Id = 201,
                Name = "Sword",
                Weight = 2.0,
                Description = "Cool2 stuff",
                Damage = 2,
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [202] = new Weapon
            {
                Id = 202,
                Name = "Knife",
                Weight = 1.0,
                Description = "Cool stuff",
                Damage = 1,
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [203] = new Accessory
            {
                Id = 203,
                Name = "Ring",
                Weight = 0.2,
                Description = "Cool2",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [204] = new Accessory
            {
                Id = 204,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
        };
        return _items; 
    }
    public static List<Item> GetPresentItems()
    {
        return GetItems().Where(i => _presentIds.Contains(i.Key)).Select(i => i.Value).ToList();
    }
}
public class Weapon : Item
{
    public int Damage { get; set; }
    public override int MaxInStack { get; } = 1;
}
public class Accessory : Item
{
    public override int MaxInStack { get; } = 1;
}
public abstract class Item
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public abstract int MaxInStack { get; }
    public double Weight { get; set; }
    public required string Description { get; set; }
    public KeyValuePair<string, int>? Stats { get; set; }
    public required string PhotoLink { get; set; }
}