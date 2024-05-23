using RPGBot.Data;
using RPGBot.Utils.Embeds;

namespace RPGBot.UserInterface.Embeds;

public class InventoryEmbed : DefaultEmbed
{
    public InventoryEmbed(Dictionary<Item, int> items)
    {
        var itemsExist = items.Count != 0;
        var desc = itemsExist ? string.Join(
            "\n", items.Select((item, index)
                => $"**{index + 1}.** {item.Key.Name} — {item.Value}")
        ) : "**Currently you have no items**";
        Title = "Inventory";
        Description = desc;
    }
}
