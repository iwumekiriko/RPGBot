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
public abstract partial class Player
{
    public ulong UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    public ulong GuildId { get; set; }
    [ForeignKey("GuildId")]
    public Guild Guild { get; set; }

    public bool isStarted { get; set; }
    public string ClassType { get; set; }
    public abstract int Health { get; set; }
    public abstract int Armor { get; set; }
    public abstract int Strength { get; set; }
    public abstract int Dexterity { get; set; }
    public abstract int Intellect { get; set; }
    public abstract int Memory { get; set; }
    public abstract int Conviction { get; set; }

    public int Money { get; set; }
}
public partial class Warrior : Player
{
    public override int Health { get; set; } = 20;
    public override int Armor { get; set; } = 5;
    public override int Strength { get; set; } = 13;
    public override int Dexterity { get; set; } = 9;
    public override int Intellect { get; set; } = 8;
    public override int Memory { get; set; } = 3;
    public override int Conviction { get; set; } = 1;
}
public partial class Hunter : Player
{
    public override int Health { get; set; } = 20;
    public override int Armor { get; set; } = 4;
    public override int Strength { get; set; } = 8;
    public override int Dexterity { get; set; } = 13;
    public override int Intellect { get; set; } = 9;
    public override int Memory { get; set; } = 4;
    public override int Conviction { get; set; } = 1;
}
public partial class Mage : Player
{
    public override int Health { get; set; } = 20;
    public override int Armor { get; set; } = 2;
    public override int Strength { get; set; } = 9;
    public override int Dexterity { get; set; } = 8;
    public override int Intellect { get; set; } = 13;
    public override int Memory { get; set; } = 5;
    public override int Conviction { get; set; } = 1;
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

    public string Name { get; set; }
    public string Type { get; set; }    
    public abstract int MaxInStack { get; }
    public double Weight { get; set; }
    public string Description { get; set; }
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