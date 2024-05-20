using RPGBot.Database;
using RPGBot.Utils.Embeds;

namespace RPGBot.UserInterface.Embeds;

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
