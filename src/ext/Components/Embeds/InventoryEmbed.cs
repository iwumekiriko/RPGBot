using RPGBot.Utils;
using RPGBot.Database;

namespace RPGBot.Components.Embeds;

internal class InventoryEmbed : DefaultEmbed
{
    public InventoryEmbed(List<Item> items)
    {
        var desc = string.Join(
            "\n", items.Select((item, index)
                => $"**{index + 1}**. {item.Name}")
        );

        Title = "Inventory";
        Description = desc;
    }
}
