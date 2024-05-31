using Discord;

namespace RPGBot.UserInterface.Buttons;

public class DropItemButton : ButtonBuilder
{
    public DropItemButton()
    {
        Label = "Drop Item";
        Style = ButtonStyle.Danger;
        CustomId = "dropItemButton";
    }
}
