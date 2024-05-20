using Discord;
using RPGBot.Database;

namespace RPGBot.Components.SelectMenues;

public class QuestBoardSelectMenu : SelectMenuBuilder
{
    public QuestBoardSelectMenu(Dictionary<Quest, bool> quests)
    {
        Placeholder = "Select Quest";
        CustomId = "questBoardSelectMenu";
        MinValues = 1;
        MaxValues = 1;
        if (quests.Count == 0)
        {
            AddOption(
                label: "Empty",
                value: "None"
            );
            IsDisabled = true;
        }
        else
        {
            foreach (var quest in quests)
                AddOption(
                    label: quest.Key.Name,
                    value: quest.Key.Id.ToString()
                );
        }
    }
}

