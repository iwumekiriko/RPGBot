using Discord;

namespace RPGBot.UserInterface.Buttons;

public class BackToShopButton : ButtonBuilder
{
    public BackToShopButton()
    {
        Label = "Back";
        Style = ButtonStyle.Secondary;
        CustomId = "backToShopButton";
    }
}
