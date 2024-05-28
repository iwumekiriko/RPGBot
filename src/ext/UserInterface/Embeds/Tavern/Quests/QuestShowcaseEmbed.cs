using RPGBot.Data;
using RPGBot.Utils.Embeds;
using System.Text;

namespace RPGBot.UserInterface.Embeds;

public class QuestShowcaseEmbed : DefaultEmbed
{
    public QuestShowcaseEmbed(Quest quest)
    {
        var sb = new StringBuilder();
        sb.Append($"**Required level: **{quest.RequiredLevel}\n");
        sb.Append($"**Task:** {quest.ShortDescription}\n");
        if (quest.ItemId != null)
            sb.Append($"**Item reward: **{quest.ItemId}\n");
        if (quest.MoneyReward != 0)
            sb.Append($"**Money reward: **{quest.MoneyReward}\n");
        sb.Append($"**Experience reward: **{quest.ExpReward}\n\n");
        sb.Append($"**Description:**\n{quest.FullDescription}");

        Title = quest.Name;
        Description = sb.ToString();
    }
}
