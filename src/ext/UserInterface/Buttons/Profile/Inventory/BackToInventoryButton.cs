using Discord;

namespace RPGBot.UserInterface.Buttons;

public class BackToInventoryButton : ButtonBuilder
{
    public BackToInventoryButton()
    {
        Label = "Back";
        Style = ButtonStyle.Secondary;
        CustomId = "backToInventoryButton";
    }
}