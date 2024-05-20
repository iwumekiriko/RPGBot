using Discord.Interactions;
using RPGBot.UserInterface.Embeds;
using RPGBot.UserInterface;
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
            components: new InventoryComponent(items).Build(),
            ephemeral: true
        );
    }

    [ComponentInteraction("journalButton")]
    public async Task JournalHandler()
    {

    }
}
