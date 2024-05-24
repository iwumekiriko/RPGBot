using RPGBot.Data;
using RPGBot.Utils.Embeds;
using System.Diagnostics.Metrics;
using System.Resources;

namespace RPGBot.UserInterface.Embeds;

public class ClassShowcaseEmbed : DefaultEmbed
{
    public ClassShowcaseEmbed(string photoLink)
    {
        ImageUrl = photoLink;
    }
}