using System.ComponentModel.DataAnnotations.Schema;

namespace RPGBot.Database.Models;
public partial class InventoryItem
{
    public ulong UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    public ulong GuildId { get; set; }
    [ForeignKey("GuildId")]
    public Guild Guild { get; set; }

    public int ItemId { get; set; }
    public int Amount { get; set; }
}