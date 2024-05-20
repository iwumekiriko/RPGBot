using Discord;
using RPGBot.Data;

namespace RPGBot.Components.SelectMenues;

public class ClassSelectMenu : SelectMenuBuilder
{
    public ClassSelectMenu()
    {
        Placeholder = "Select Class";
        CustomId = "classSelectMenu";
        MinValues = 1;
        MaxValues = 1;

        foreach (var gameClass in Classes.GetClasses())
            AddOption(
                label: gameClass.Value.Name,
                value: gameClass.Key.ToString()
            );
    }
}