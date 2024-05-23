using System.ComponentModel.DataAnnotations.Schema;

namespace RPGBot.Database.Models;

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