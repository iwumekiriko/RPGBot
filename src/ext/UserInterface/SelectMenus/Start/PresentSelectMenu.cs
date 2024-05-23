using Discord;
using RPGBot.Data;

namespace RPGBot.UserInterface.SelectMenus;

public class PresentSelectMenu : SelectMenuBuilder
{
    public PresentSelectMenu(int[] presentIds)
    {
        Placeholder = "Select Present";
        CustomId = "presentSelectMenu";
        MinValues = 1;
        MaxValues = 1;

        var items = Items.GetItems();
        foreach (var id in presentIds)
        {
            var gamePresent = items[id];

            AddOption(
                label: gamePresent.Name,
                value: id.ToString()
            );
        }
            
    }
}
