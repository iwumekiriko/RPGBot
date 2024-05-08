using Discord;
using RPGBot.Database;

namespace RPGBot.Components.SelectMenues;

public class InventorySelectMenu : SelectMenuBuilder
{
    public InventorySelectMenu(Dictionary<Item, int> items)
    {
        Placeholder = "Select Item";
        CustomId = "inventorySelectMenu";
        MinValues = 1;
        MaxValues = 1;

        foreach (var item in items)
            AddOption(
                label: item.Key.Name,
                value: item.Key.Id.ToString()
            );
    }
}

