using JsonProperty.EFCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGBot.Database;
public partial class Guild
{
    [Key]
    public ulong Id { get; set; }

    [MaxLength(2)]
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

    public int StartPhase { get; set; }
    public int ClassId { get; set; }
    public int Health { get; set; }
    public int Armor { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Intellect { get; set; }
    public int Memory { get; set; }
    public int Conviction { get; set; }

    public int Money { get; set; } = 0;
    public int Experience { get; set; } = 0;
    public int Level { get; set; } = 1;
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
    public string? Reward { get; set; }
}
public partial class Weapon : Item
{
    public int Damage { get; set; }
    public override int MaxInStack { get; } = 1;
}
public partial class Accessory : Item
{
    public int Uses { get; set; }
    public int UsesLeft { get; set; }
    public override int MaxInStack { get; } = 1;

    public Accessory() : this(0) { }

    public Accessory(int uses)
    {
        Uses = uses;
        UsesLeft = uses;
    }
}
public abstract partial class Item
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }
    public string Type { get; set; }    
    public abstract int MaxInStack { get; }
    public double Weight { get; set; }
    public required string Description { get; set; }
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
public partial class Quest
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string ShortDescription { get; set; }
    public required string FullDescription { get; set; }
    public int RequiredLevel { get; set; }
    public int? ItemId { get; set; }
    [ForeignKey("ItemId")]
    public Item? ItemReward { get; set; }

    public int ExpReward { get; set; }
    public int MoneyReward { get; set; }
    public int NeededToComplete { get; set; }
}
public partial class QuestBoardItem
{
    public ulong UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    public ulong GuildId { get; set; }
    [ForeignKey("GuildId")]
    public Guild Guild { get; set; }

    public int QuestId { get; set; }
    [ForeignKey("QuestId")]
    public Quest Quest { get; set; }

    public bool IsStarted { get; set; }
    public bool IsFinished { get; set; }
    public int Progress { get; set; } = 0;
}