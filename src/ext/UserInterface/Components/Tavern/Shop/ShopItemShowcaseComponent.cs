using Discord;

using RPGBot.Data;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface;

public class ShopItemShowcaseComponent : ComponentBuilder
{
    public ShopItemShowcaseComponent()
        => WithButton(new BuyButton())
          .WithButton(new BackToShopButton());
}
