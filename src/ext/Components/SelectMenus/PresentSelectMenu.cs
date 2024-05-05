using Discord;
using RPGBot.Data;

namespace RPGBot.Components.SelectMenues;

public class PresentSelectMenu : SelectMenuBuilder
{
    public PresentSelectMenu()
    {
        Placeholder = "Select Mom's Present";
        CustomId = "presentSelectMenu";
        MinValues = 1;
        MaxValues = 1;

        foreach (var gamePresent in Enum.GetValues(typeof(Presents)))
            AddOption(
                label: gamePresent.ToString(),
                value: ((int)gamePresent).ToString()
            );
    }
}

