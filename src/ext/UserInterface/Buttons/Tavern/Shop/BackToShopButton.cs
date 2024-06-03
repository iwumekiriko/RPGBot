using Discord;

namespace RPGBot.UserInterface.Buttons;

public class BackToShopButton : ButtonBuilder
{
    public BackToShopButton()
    {
        Label = "Back to shop";
        Style = ButtonStyle.Secondary;
        CustomId = "backToShopButton";
    }
}
