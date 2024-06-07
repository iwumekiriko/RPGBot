using RPGBot.UserInterface.Buttons;
using RPGBot.Utils.Embeds;

namespace RPGBot.UserInterface.Embeds;

public class ArchivesShowcaseEmbed : DefaultEmbed
{
    public ArchivesShowcaseEmbed(string id)
    {
        switch (id)
        {
            case "enemy":
            {
                Title = "Enemy";
                break;
            }
            case "locations":
            {
                Title = "Locations";
                break;
            }
            case "npc":
            {
                Title = "NPC";
                break;
            }
            case "items":
            {
                Title = "Items";
                break;
            }
            case "skills":
            {
                Title = "Skills";
                break;
            }
        }

        ImageUrl = "https://imgur.com/eYhbbYJ.png";
    }
}
