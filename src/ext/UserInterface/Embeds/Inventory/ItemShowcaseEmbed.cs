using RPGBot.Data;
using System.Text;
using RPGBot.Utils.Embeds;

namespace RPGBot.UserInterface.Embeds;

public class ItemShowcaseEmbed : DefaultEmbed
{
    public ItemShowcaseEmbed(string photoLink)
    {
        ImageUrl = photoLink;
    }
}