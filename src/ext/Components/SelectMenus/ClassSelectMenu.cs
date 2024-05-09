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

        var classes = new Dictionary<string, int>()
        {
            { "Warrior", 101 },
            { "Hunter", 102 },
            { "Mage", 103 }
        };

        foreach (var gameClass in classes)
            AddOption(
                label: gameClass.Key,
                value: gameClass.Value.ToString()
            );
    }
}

