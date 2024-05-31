using Discord;

namespace RPGBot.UserInterface.Buttons;

public class BuyButton : ButtonBuilder
{
    public BuyButton()
    {
        Label = "Buy";
        Style = ButtonStyle.Success;
        CustomId = "buyButton";
    }
}
