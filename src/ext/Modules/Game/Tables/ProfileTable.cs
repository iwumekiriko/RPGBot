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
        var items = Items.GetItems();

        var playerItems = _database.Inventory
            .Where(i => i.UserId == Context.User.Id &&
                        i.GuildId == Context.Guild.Id &&
                        i.Amount != 0)
            .Select(i => new { i.ItemId, i.Amount })
            .ToDictionary(i => items[i.ItemId], i => i.Amount);

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
