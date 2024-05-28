using Discord;
using RPGBot.Data;

namespace RPGBot.UserInterface.SelectMenues;

public class QuestBoardSelectMenu : SelectMenuBuilder
{
    public QuestBoardSelectMenu(List<Quest> quests)
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
                    label: quest.Name,
                    value: quest.Id.ToString()
                );
        }
    }
}

