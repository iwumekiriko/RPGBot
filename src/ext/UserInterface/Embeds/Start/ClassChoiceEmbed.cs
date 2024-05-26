using RPGBot.Utils.Embeds;

namespace RPGBot.UserInterface.Embeds;

public class ClassChoiceEmbed : DefaultEmbed
{
    public ClassChoiceEmbed(string fileName)
    {
        ImageUrl = $"attachment://{fileName}";
    }
}
