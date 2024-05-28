using Discord.Interactions;

using RPGBot.UserInterface;
using RPGBot.UserInterface.Embeds;
using RPGBot.Data;
using RPGBot.Services;
using System.Data;

namespace RPGBot.Modules.Game;

public partial class GameModule
{
    private static int CurrentItemId { get; set; }

    [ComponentInteraction("inventorySelectMenu")]
    public async Task ItemShowcase(string[] selections)
    {
        var itemId = Int32.Parse(selections.First());
        var item = _inventory.GetItem(itemId) ??
            throw new InvalidDataException();
        CurrentItemId = itemId;
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new ItemShowcaseEmbed(item).Build();
            message.Components = new ItemShowcaseComponent().Build();
        });
    }
    [ComponentInteraction("inventoryBackButton")]
    public async Task InventoryBack()
    {
        var player = await GetOrCreatePlayerAsync();
        var playerItems = await _inventory.GetPlayerItems(player);
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new InventoryEmbed(playerItems).Build();
            message.Components = new InventoryComponent(playerItems).Build();
        });
    }
    [ComponentInteraction("dropItemButton")]
    public async Task DropItem()
    {
        var player = await GetOrCreatePlayerAsync();
        _inventory.DropItemFromInventory(player, CurrentItemId);
        await DeferAsync();
        await DeleteOriginalResponseAsync();
    }
}
