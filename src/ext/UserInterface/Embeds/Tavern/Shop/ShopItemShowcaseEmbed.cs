using RPGBot.Data;
using RPGBot.Utils.Embeds;
using static System.Net.WebRequestMethods;

namespace RPGBot.UserInterface.Embeds;

public class ShopItemShowcaseEmbed : DefaultEmbed
{
    public ShopItemShowcaseEmbed(Item item)
    {
        Title = item.Name;
        ThumbnailUrl = "https://imgur.com/6hQ041d.png";

        if (item is Weapon weapon)
        {
            Description = "``` Weapon ```";
            AddField("Damage", $"{weapon.Damage} d/h", true);
        }
        if (item is Accessory accessory)
        {
            Description = "``` Accessory ```";
        }

        AddField("Weight", $"{item.Weight} kg", true);
        AddField("Description", item.Description, false);
        //ImageUrl = item.PhotoLink;
    }
}