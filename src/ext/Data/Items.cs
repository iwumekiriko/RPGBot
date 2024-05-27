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
            [205] = new Accessory
            {
                Id = 205,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [206] = new Accessory
            {
                Id = 206,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [207] = new Accessory
            {
                Id = 207,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [208] = new Accessory
            {
                Id = 208,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [209] = new Accessory
            {
                Id = 209,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [210] = new Accessory
            {
                Id = 210,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [211] = new Accessory
            {
                Id = 211,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [212] = new Accessory
            {
                Id = 212,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [213] = new Accessory
            {
                Id = 213,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [214] = new Accessory
            {
                Id = 214,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [215] = new Accessory
            {
                Id = 215,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [216] = new Accessory
            {
                Id = 216,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [217] = new Accessory
            {
                Id = 217,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [218] = new Accessory
            {
                Id = 218,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [219] = new Accessory
            {
                Id = 220,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [221] = new Accessory
            {
                Id = 221,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [222] = new Accessory
            {
                Id = 222,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [223] = new Accessory
            {
                Id = 223,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [224] = new Accessory
            {
                Id = 224,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [225] = new Accessory
            {
                Id = 225,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [226] = new Accessory
            {
                Id = 226,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [227] = new Accessory
            {
                Id = 227,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [228] = new Accessory
            {
                Id = 228,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [229] = new Accessory
            {
                Id = 229,
                Name = "Scroll",
                Weight = 0.1,
                Description = "Cool212313",
                PhotoLink = "https://imgur.com/ejzLAcq.png"
            },
            [230] = new Accessory
            {
                Id = 230,
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