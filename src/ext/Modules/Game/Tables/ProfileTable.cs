    using Discord.Interactions;

using RPGBot.UserInterface.Embeds;
using RPGBot.UserInterface;
using RPGBot.Data;
using RPGBot.Modules.Game.Services;

namespace RPGBot.Modules.Game;

public class ProfileTable(IServiceProvider services) : BaseModule(services)
{
    [ComponentInteraction("inventoryButton")]
    public async Task InventoryHandler()
    {
        var player = await GetOrCreatePlayerAsync();
        var playerItems = await _inventory.GetPlayerItems(player);
        await DeferAsync();
        await FollowupAsync(
            embed: new InventoryEmbed(playerItems).Build(),
            components: new InventoryComponent(playerItems).Build(),
            ephemeral: true
        );
    }
    [ComponentInteraction("journalButton")]
    public async Task JournalHandler()
    {

    }
}
