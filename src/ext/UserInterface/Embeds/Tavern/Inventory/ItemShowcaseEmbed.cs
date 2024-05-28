using RPGBot.Data;
using RPGBot.Utils.Embeds;

namespace RPGBot.UserInterface.Embeds;

public class ItemShowcaseEmbed : DefaultEmbed
{
    public ItemShowcaseEmbed(Item item)
    {
        //Title = item.Name;
        //ThumbnailUrl = item.PhotoLink;

        //if (item is Weapon weapon)
        //{
        //    Description = "``` Weapon ```";
        //    AddField("Damage", weapon.Damage, true);
        //}
        //if (item is Accessory accessory)
        //{
        //    Description = "``` Accessory ```";
        //}

        //AddField("Weight", item.Weight, true);
        //AddField("Stats", "Health +2", true);
        //AddField("Description", item.Description, false);
        ImageUrl = item.PhotoLink;
    }
}