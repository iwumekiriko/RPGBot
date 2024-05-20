using Discord;

namespace RPGBot.UserInterface.Buttons;

public class ShopButton : ButtonBuilder
{
    public ShopButton()
    {
        Label = "Shop";
        Style = ButtonStyle.Primary;
        CustomId = "shopButton";
    }
}
