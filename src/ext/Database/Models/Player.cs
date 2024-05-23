using System.ComponentModel.DataAnnotations.Schema;

namespace RPGBot.Database.Models;

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