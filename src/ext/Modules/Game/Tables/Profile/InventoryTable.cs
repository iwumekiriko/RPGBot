using Discord.Interactions;

using RPGBot.UserInterface;
using RPGBot.UserInterface.Embeds;
using RPGBot.UserInterface.Modals;
using RPGBot.Modules.Game.Services;

namespace RPGBot.Modules.Game;

public class InventoryTable(IServiceProvider services) : BaseModule(services)
{
    private static int CurrentItemId { get; set; }

    [ComponentInteraction("inventorySelectMenu")]
    public async Task InventoryItemShowcase(string[] selections)
    {
        var itemId = int.Parse(selections.First());
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
        var accepted = await _inventory.DropItemFromInventory(player, CurrentItemId);
        if (accepted)
        {
            var playerItems = await _inventory.GetPlayerItems(player);
            await DeferAsync();
            await ModifyOriginalResponseAsync(message =>
            {
                message.Embed = new InventoryEmbed(playerItems).Build();
                message.Components = new InventoryComponent(playerItems).Build();
            });
        }
        else
        {
            await RespondAsync("Not enough items");
        }
            
    }
    [ComponentInteraction("dropItemsButton")]
    public async Task GetAmount()
    {
        await RespondWithModalAsync<DropItemsModal>("dropItemsModal");
    } 
    [ModalInteraction("dropItemsModal")]
    public async Task DropItems(DropItemsModal modal)
    {
        var player = await GetOrCreatePlayerAsync();
        if (int.TryParse(modal.Amount, out int amount))
        {
            if (await _inventory.DropItemFromInventory(player, CurrentItemId, amount))
            {
                var playerItems = await _inventory.GetPlayerItems(player);
                await DeferAsync();
                await ModifyOriginalResponseAsync(message =>
                {
                    message.Embed = new InventoryEmbed(playerItems).Build();
                    message.Components = new InventoryComponent(playerItems).Build();
                });
            }
            else
            {
                await DeferAsync();
                await FollowupAsync("Not enough items", ephemeral: true);
            }
        }
        else
        {
            await DeferAsync();
            await FollowupAsync("Failed to convert input to a number", ephemeral: true);
        }
    }
    [ComponentInteraction("useItemButton")]
    public async Task UseItem()
    {
        var player = await GetOrCreatePlayerAsync();
        var playerItems = await _inventory.GetPlayerItems(player);
        await DeferAsync();
        await FollowupAsync(
            "You used the item...",
            ephemeral: true
        );
    }
}
