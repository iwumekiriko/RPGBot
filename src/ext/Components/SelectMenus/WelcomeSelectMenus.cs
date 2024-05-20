using Discord;
using RPGBot.Data;

namespace RPGBot.Components.SelectMenus;

public class PresentSelectMenu : SelectMenuBuilder
{
    public PresentSelectMenu()
    {
        Placeholder = "Select Mom's Present";
        CustomId = "presentSelectMenu";
        MinValues = 1;
        MaxValues = 1;

        var presents = new Dictionary<string, int>()
    {
        { "Sword", 201 },
        { "Knife", 202},
        { "Ring", 203}
    };

        foreach (var gamePresent in presents)
            AddOption(
                label: gamePresent.Key,
                value: gamePresent.Value.ToString()
            );
    }
}
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
