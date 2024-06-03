using RPGBot.Data;
using RPGBot.Database.Models;
using RPGBot.Utils.Embeds;

namespace RPGBot.UserInterface.Embeds;

internal class NotesEmbed : DefaultEmbed
{
    public NotesEmbed(Dictionary<Quest, Tuple<int, bool>> quests)
    {
        var questsExist = quests.Count != 0;
        var desc = questsExist ? string.Join(
            "\n", quests.Select((quest, index)
                => quest.Value.Item2 ? $"~~**{index + 1}.** {quest.Key.Name}~~" : 
                    $"**{index + 1}.** {quest.Key.Name} - {quest.Value.Item1}/{quest.Key.NeededToComplete}")
        ) : "**Currently there are no active quests**";


        Title = "Notes";
        Description = desc;
    }
}
