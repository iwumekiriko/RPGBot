using Discord;
using RPGBot.Database;
using RPGBot.Utils.Embeds;
using System.Text;

namespace RPGBot.Components.Embeds;

internal class QuestBoardEmbed : DefaultEmbed
{
    public QuestBoardEmbed(Dictionary<Quest, bool> quests)
    {
        var questsExist = quests.Count != 0;
        var desc = questsExist ? string.Join(
            "\n", quests.Select((quest, index)
                => $"**{index + 1}.** {quest.Key.Name}")
        ) : "**Currently there are no quests**";

        Title = "Quest Board";
        Description = desc;
    }
}
public class QuestShowcaseEmbed : DefaultEmbed
{
    public QuestShowcaseEmbed(Quest quest)
    {
        var sb = new StringBuilder();
        sb.Append($"**Required level: **{quest.RequiredLevel}\n");
        sb.Append($"**Task:** {quest.ShortDescription}\n");
        if (quest.ItemReward != null)
            sb.Append($"**Item reward: **{quest .ItemReward.Name}\n");
        if (quest.MoneyReward != 0)
            sb.Append($"**Money reward: **{quest.MoneyReward}\n");
        sb.Append($"**Experience reward: **{quest.ExpReward}\n\n");
        sb.Append($"**Description:**\n{quest.FullDescription}");

        Title = quest.Name;
        Description = sb.ToString();
    }
}
