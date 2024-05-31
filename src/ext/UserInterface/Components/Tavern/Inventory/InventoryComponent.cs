using Discord;
using RPGBot.UserInterface.SelectMenus;
using RPGBot.Data;

namespace RPGBot.UserInterface;

public class InventoryComponent : ComponentBuilder
{
    public InventoryComponent(Dictionary<Item, int> items)
        => WithSelectMenu(new InventorySelectMenu(items));
}
