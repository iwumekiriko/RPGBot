using Discord;
using RPGBot.Data;

namespace RPGBot.UserInterface.SelectMenus;

public class InventorySelectMenu : SelectMenuBuilder
{
    public InventorySelectMenu(Dictionary<Item, int> items)
    {
        Placeholder = "Select Item";
        CustomId = "inventorySelectMenu";
        MinValues = 1;
        MaxValues = 1;
        if (items.Count == 0) 
        {
            AddOption(
                label: "Empty",
                value: "None"
            );
            IsDisabled = true;
        }
        else
        {
            foreach (var item in items)
                AddOption(
                    label: item.Key.Name,
                    value: item.Key.Id.ToString()
                );
        }
    }
}

