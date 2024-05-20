using Discord;
using RPGBot.UserInterface.SelectMenues;
using RPGBot.Database;

namespace RPGBot.UserInterface;

public class InventoryComponent : ComponentBuilder
{
    public InventoryComponent(Dictionary<Item, int> items)
        => WithSelectMenu(new InventorySelectMenu(items));
}
