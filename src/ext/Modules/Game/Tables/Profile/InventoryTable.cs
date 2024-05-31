using Discord.Interactions;

using RPGBot.UserInterface;
using RPGBot.UserInterface.Embeds;
using RPGBot.Modules.Game.Services;

namespace RPGBot.Modules.Game;

public class InventoryTable(IServiceProvider services) : BaseModule(services)
{
    private static int CurrentItemId { get; set; }

    [ComponentInteraction("inventorySelectMenu")]
    public async Task InventoryItemShowcase(string[] selections)
    {
        var itemId = Int32.Parse(selections.First());
        var item = _inventory.GetItem(itemId) ??
            throw new InvalidDataException();
        CurrentItemId = itemId;
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new InventoryItemShowcaseEmbed(item).Build();
            message.Components = new InventoryItemShowcaseComponent().Build();
        });
    }
    [ComponentInteraction("backToInventoryButton")]
    public async Task BackToInventory()
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
        await _inventory.DropItemFromInventory(player, CurrentItemId);
        await DeferAsync();
        await DeleteOriginalResponseAsync();
    }
}
