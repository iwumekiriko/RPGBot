using JsonProperty.EFCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGBot.Database;
public partial class Guild
{
    [Key]
    public ulong Id { get; set; }

    [MaxLength(10)]
    public string? Locale { get; set; }
}
public partial class User
{
    [Key]
    public ulong Id { get; set; }
}
public partial class Player
{
    public ulong UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    public ulong GuildId { get; set; }
    [ForeignKey("GuildId")]
    public Guild Guild { get; set; }

    public bool IsStarted { get; set; }
    public int ClassId { get; set; }
    public int PresentId {  get; set; }
}
public partial class Enemy
{
    [Key]
    public int Id { get; set; }

    public string Name {  get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int? GainedExp { get; set;}
    public string? Description { get; set; }
}
public partial class Weapon : Item
{
    public int Damage { get; set; }
}
public partial class Accessory : Item
{
    public int UsesLeft { get; set; }
}
public class Item
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    public string Type { get; set; }
    public double? Weight { get; set; }
    public string? Description { get; set; }
    public JsonDictionary Stats { get; set; } = new();
}

public partial class Inventory
{
    public ulong UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    public ulong GuildId { get; set; }
    [ForeignKey("GuildId")]
    public Guild Guild { get; set; }

    public int ItemId { get; set; }
    [ForeignKey("ItemId")]
    public Item Item { get; set; }

    public int Amount { get; set; }
}