using System.ComponentModel.DataAnnotations.Schema;

namespace RPGBot.Database.Models;
public partial class EquipmentItem
{
    public ulong UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    public ulong GuildId { get; set; }
    [ForeignKey("GuildId")]
    public Guild Guild { get; set; }

    public int ItemId { get; set; }
    public int Slot { get; set; }
}