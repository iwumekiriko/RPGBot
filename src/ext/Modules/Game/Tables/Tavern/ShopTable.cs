using Discord.Interactions;

using RPGBot.Modules.Game.Services;
using RPGBot.UserInterface.Embeds;
using RPGBot.UserInterface;
using System.ComponentModel.DataAnnotations;

namespace RPGBot.Modules.Game;
public class ShopTable(IServiceProvider services) : BaseModule(services)
{
    private static int CurrentItemId;

    [ComponentInteraction("shopSelectMenu")]
    public async Task ShopItemShowcase(string[] selections)
    {
        var itemId = Int32.Parse(selections.First());
        var item = _inventory.GetItem(itemId) ??
            throw new InvalidDataException();
        CurrentItemId = itemId;
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new ShopItemShowcaseEmbed(item).Build();
            message.Components = new ShopItemShowcaseComponent().Build();
        });
    }
    [ComponentInteraction("buyButton")]
    public async Task BuyShopItem()
    {
        var player = await GetOrCreatePlayerAsync();
        var response = await _shop.AcceptBuyItem(player, CurrentItemId) ?
            "Item bought" : "Not enough money";
        await RespondAsync(response, ephemeral: true);
    }
    [ComponentInteraction("backToShopButton")]
    public async Task BackToShop()
    {
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new ShopEmbed().Build();
            message.Components = new ShopComponent().Build();
        });
    }
}
