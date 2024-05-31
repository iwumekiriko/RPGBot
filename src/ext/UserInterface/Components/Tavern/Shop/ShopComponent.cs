using Discord;

using RPGBot.Data;
using RPGBot.UserInterface.SelectMenus;

namespace RPGBot.UserInterface;

public class ShopComponent : ComponentBuilder
{
    public ShopComponent()
        => WithSelectMenu(new ShopSelectMenu());
}
