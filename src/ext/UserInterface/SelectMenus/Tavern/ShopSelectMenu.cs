using Discord;
using RPGBot.Data;

namespace RPGBot.UserInterface.SelectMenus;

public class ShopSelectMenu : SelectMenuBuilder
{
    public ShopSelectMenu()
    {
        Placeholder = "Select item to buy";
        CustomId = "shopSelectMenu";
        MinValues = 1;
        MaxValues = 1;
        foreach (var item in Items.GetShopItems())
            AddOption(
                label: item.Name,
                description: $"price: {item.Cost}",
                value: item.Id.ToString()
            );
    }
}

