using Discord;

namespace RPGBot.UserInterface.Buttons;

public class DropItemsButton : ButtonBuilder
{
    public DropItemsButton()
    {
        Label = "Drop Items";
        Style = ButtonStyle.Danger;
        CustomId = "dropItemsButton";
    }
}
