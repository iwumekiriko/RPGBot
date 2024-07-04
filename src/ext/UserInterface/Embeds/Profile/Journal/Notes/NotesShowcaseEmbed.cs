using RPGBot.Data;
using RPGBot.Utils.Embeds;
using System.Text;

namespace RPGBot.UserInterface.Embeds;

public class NotesShowcaseEmbed : DefaultEmbed
{
    public NotesShowcaseEmbed(KeyValuePair<Quest, Tuple<int, bool, bool>> quest)
    {
        var sb = new StringBuilder();
        sb.Append($"**Required level: **{quest.Key.RequiredLevel}\n");
        sb.Append($"**Task:** {quest.Key.ShortDescription}\n");
        if (quest.Key.ItemId != null)
            sb.Append($"**Item reward: **{quest.Key.ItemId}\n");
        if (quest.Key.MoneyReward != 0)
            sb.Append($"**Money reward: **{quest.Key.MoneyReward}\n");
        sb.Append($"**Experience reward: **{quest.Key.ExpReward}\n");
        if (quest.Value.Item3)
            sb.Append($"**Status: **Finished\n\n");
        else if (quest.Value.Item1 >= quest.Key.NeededToComplete)
            sb.Append($"**Status: **Completed\n\n");
        else
            sb.Append($"**Status: **In progress ({quest.Value.Item1}/{quest.Key.NeededToComplete})\n\n");
        sb.Append($"**Description:**\n{quest.Key.FullDescription}");

        Title = quest.Key.Name;
        Description = sb.ToString();
    }
}
