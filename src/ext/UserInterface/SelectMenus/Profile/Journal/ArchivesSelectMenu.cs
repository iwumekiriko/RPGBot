using Discord;
using RPGBot.Data;

namespace RPGBot.UserInterface.SelectMenues;

public class ArchivesSelectMenu : SelectMenuBuilder
{
    public ArchivesSelectMenu()
    {
        Placeholder = "Select Section";
        CustomId = "archivesSelectMenu";
        MinValues = 1;
        MaxValues = 1;
        AddOption(
            label: "Enemy",
            value: "enemy"
        );
        AddOption(
            label: "Locations",
            value: "locations"
        );
        AddOption(
            label: "NPC",
            value: "npc"
        );
        AddOption(
            label: "Items",
            value: "items"
        );
        AddOption(
            label: "Skills",
            value: "skills"
        );
    }
}

