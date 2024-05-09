using RPGBot.Utils;
using RPGBot.Database;

namespace RPGBot.Components.Embeds;

public class InventoryEmbed : DefaultEmbed
{
    public InventoryEmbed(Dictionary<Item, int> items)
    {
        var desc = string.Join(
            "\n", items.Select((item, index)
                => $"**{index + 1}.** {item.Key.Name} — {item.Value}")
        );

        Title = "Inventory";
        Description = desc;
    }
}
