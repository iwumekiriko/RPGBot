using Discord;
using RPGBot.Data;

namespace RPGBot.UserInterface.SelectMenues;

public class ActiveQuestSelectMenu : SelectMenuBuilder
{
    public ActiveQuestSelectMenu(List<Quest> quests)
    {
        Placeholder = "Select Quest";
        CustomId = "activeQuestSelectMenu";
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

