using RPGBot.Utils.Embeds;
using static System.Net.WebRequestMethods;

namespace RPGBot.Components.Embeds;

public class WelcomeEmbed : DefaultEmbed
{
    public WelcomeEmbed()
    {
        ImageUrl = "https://imgur.com/3cr0PEn.png";
    }
}
public class ClassChoiceEmbed : DefaultEmbed
{
    public ClassChoiceEmbed()
    {
        ImageUrl = "https://imgur.com/0S10i96.png";
    }
}
public class PresentChoiceEmbed : DefaultEmbed
{
    public PresentChoiceEmbed()
    {
        ImageUrl = "https://imgur.com/S5gY3TO.png";
    }
}
public class ClassShowcaseEmbed : DefaultEmbed
{
    public ClassShowcaseEmbed(int classId)
    {
        ImageUrl = classId switch
        {
            101 => "https://imgur.com/kgeTFhq.png",
            102 => "https://imgur.com/qyYZeML.png",
            103 => "https://imgur.com/kgeTFhq.png",
            _ => throw new InvalidDataException()
        };
    }
}
