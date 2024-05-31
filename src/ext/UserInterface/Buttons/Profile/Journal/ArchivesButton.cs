using Discord;

namespace RPGBot.UserInterface.Buttons;

public class ArchivesButton : ButtonBuilder
{
    public ArchivesButton()
    {
        Label = "Archives";
        Style = ButtonStyle.Primary;
        CustomId = "archivesButton";
    }
}