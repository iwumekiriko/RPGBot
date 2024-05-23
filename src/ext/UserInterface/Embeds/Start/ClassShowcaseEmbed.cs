using RPGBot.Utils.Embeds;

namespace RPGBot.UserInterface.Embeds;

public class ClassShowcaseEmbed : DefaultEmbed
{
    public ClassShowcaseEmbed(string photoLink)
    {
        ImageUrl = photoLink;
    }
}