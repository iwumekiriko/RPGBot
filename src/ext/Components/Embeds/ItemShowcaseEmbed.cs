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
        sb.Append($"**Description** \n{item.Description}");;
        Title = item.Name;
        Description = sb.ToString();
    }
}