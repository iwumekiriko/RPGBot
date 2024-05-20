using RPGBot.Utils.Embeds;

namespace RPGBot.UserInterface.Embeds;

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
