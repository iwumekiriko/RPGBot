using Discord;

namespace RPGBot.UserInterface.Buttons;

public class UseItemButton : ButtonBuilder
{
    public UseItemButton()
    {
        Label = "Use Item";
        Style = ButtonStyle.Success;
        CustomId = "useItemButton";
    }
}