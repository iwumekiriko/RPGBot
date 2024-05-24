using Discord;
using RPGBot.Data;

namespace RPGBot.UserInterface.SelectMenus;

public class PresentSelectMenu : SelectMenuBuilder
{
    public PresentSelectMenu()
    {
        Placeholder = "Select Present";
        CustomId = "presentSelectMenu";
        MinValues = 1;
        MaxValues = 1;

        var items = Items.GetPresentItems();
        foreach (var gamePresent in items)
        {
            AddOption(
                label: gamePresent.Name,
                value: gamePresent.Id.ToString()
            );
        }
            
    }
}
