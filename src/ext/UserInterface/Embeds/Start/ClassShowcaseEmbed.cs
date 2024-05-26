using Discord;
using RPGBot.Utils.Embeds;

namespace RPGBot.UserInterface.Embeds;

public class ClassShowcaseEmbed : DefaultEmbed
{
    public ClassShowcaseEmbed(string fileName)
    {
        ImageUrl = $"attachment://{fileName}";
    }
}