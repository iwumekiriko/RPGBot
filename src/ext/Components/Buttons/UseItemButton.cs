using Discord;

namespace RPGBot.Components.Buttons;

public class UseItemButton : ButtonBuilder
{
    public UseItemButton()
    {
        Label = "Use Item";
        Style = ButtonStyle.Success;
        CustomId = "useItemButton";
    }
}