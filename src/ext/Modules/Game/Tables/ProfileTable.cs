using Discord.Interactions;
using RPGBot.Components.Embeds;
using RPGBot.Components;
using RPGBot.Modules.Game.Services;

namespace RPGBot.Modules.Game;

public class ProfileTable(IServiceProvider services) : BaseModule(services)
{
    [ComponentInteraction("inventoryButton")]
    public async Task InventoryHandler()
    {
        var items = _inventory
            .GetPlayerInventory(Context.Guild.Id, Context.User.Id);

        await DeferAsync();
        await FollowupAsync(
            embed: new InventoryEmbed(items).Build(),
            components: new InventoryComponents(items).Build(),
            ephemeral: true
        );
    }

    [ComponentInteraction("journalButton")]
    public async Task JournalHandler()
    {

    }
}
