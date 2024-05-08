using Discord;
using RPGBot.Components.Buttons;
using RPGBot.Components.SelectMenues;
using RPGBot.Database;

namespace RPGBot.Components
{
    public class InventoryComponents : ComponentBuilder
    {
        public InventoryComponents(Dictionary<Item, int> items)
            => WithSelectMenu(new InventorySelectMenu(items));
    }
    public class ItemShowcaseComponents : ComponentBuilder
    {
        public ItemShowcaseComponents()
            => WithButton(new UseItemButton())
              .WithButton(new InventoryBackButton());
    }
}