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

        foreach (var gameClass in Enum.GetValues(typeof(Classes)))
            AddOption(
                label: gameClass.ToString(),
                value: ((int)gameClass).ToString()
            );
    }
}

