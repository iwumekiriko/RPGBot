using RPGBot.Data;
using RPGBot.Utils.Embeds;

namespace RPGBot.UserInterface.Embeds;

internal class NotesEmbed : DefaultEmbed
{
    public NotesEmbed(List<Quest> quests)
    {
        var questsExist = quests.Count != 0;
        var desc = questsExist ? string.Join(
            "\n", quests.Select((quest, index)
                => $"**{index + 1}.** {quest.Name}")
        ) : "**Currently there are no active quests**";

        Title = "Notes";
        Description = desc;
    }
}
