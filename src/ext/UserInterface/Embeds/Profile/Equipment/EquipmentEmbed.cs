using RPGBot.Data;
using RPGBot.Utils.Embeds;

namespace RPGBot.UserInterface.Embeds;

public class EquipmentEmbed : DefaultEmbed
{
    public EquipmentEmbed(Dictionary<int, Item> equipment)
    {
        string weapon = "";
        for (int i = 1; i <= 2; i++)
        {
            try
            {
                weapon += $"**{i}.** - {equipment[i].Name}\n";
            }
            catch
            {
                weapon += $"**{i}.** - Empty\n";
            }
        }

        string accessory = "";
        for (int i = 3; i <= 4; i++)
        {
            try
            {
                accessory += $"**{i}.** - {equipment[i].Name}\n";
            }
            catch
            {
                accessory += $"**{i}.** - Empty\n";
            }
        }

        //if (weapon == "")
        //    weapon = "**Error**";

        Title = "Equipment";
        AddField("Weapon", $"{weapon}", false);
        AddField("Accessory", $"{accessory}", true);
    }
}
