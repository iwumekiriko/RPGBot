using Discord;

namespace RPGBot.Components.Buttons;

public class ShopButton : ButtonBuilder
{
    public ShopButton()
    {
        Label = "Shop";
        Style = ButtonStyle.Primary;
        CustomId = "shopButton";
    }
}
