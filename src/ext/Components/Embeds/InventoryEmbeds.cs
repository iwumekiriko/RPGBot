using RPGBot.Database;
using System.Text;
using RPGBot.Utils.Embeds;

namespace RPGBot.Components.Embeds;

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
public class ItemShowcaseEmbed : DefaultEmbed
{
    public ItemShowcaseEmbed(Item item)
    {
        var sb = new StringBuilder();
        switch (item)
        {
            case Weapon weapon:
                sb.Append($"**Damage:** {weapon.Damage} d/h\n");
                break;

            case Accessory accessory:
                if (accessory.Uses != 0)
                    sb.Append($"**Uses left:** {accessory.UsesLeft}/{accessory.Uses}\n");
                break;
        }
        sb.Append($"**Weight:** {item.Weight} kg\n\n");
        sb.Append($"**Description** \n{item.Description}"); ;
        Title = item.Name;
        Description = sb.ToString();
    }
}