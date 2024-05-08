using RPGBot.Data;
using RPGBot.Database;
using RPGBot.Utils;
using System.Text;

namespace RPGBot.Components.Embeds;

internal class ItemShowcaseEmbed : DefaultEmbed
{

    public ItemShowcaseEmbed(Item item)
    {
        var sb = new StringBuilder();
        if (item is Weapon)
        {
            var inventoryItem = (Weapon)item;
            sb.Append($"**Damage:** {inventoryItem.Damage} d/h\n");
        }
        if (item is Accessory)
        {
            var inventoryItem = (Accessory)item;

            if (inventoryItem.Uses != 0)
                sb.Append($"**Uses left:** {inventoryItem.UsesLeft}/{inventoryItem.Uses}\n");
        }
        sb.Append($"**Weight:** {item.Weight} kg\n\n");
        sb.Append($"**Description** \n{item.Description}");;
        Title = item.Name;
        Description = sb.ToString();
    }
}