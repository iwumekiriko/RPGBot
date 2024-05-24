using Discord.Interactions;
using RPGBot.UserInterface;
using RPGBot.UserInterface.Embeds;
using RPGBot.Data;

namespace RPGBot.Modules.Game;

public partial class GameModule
{
    private static int CurrentItemId { get; set; }

    [ComponentInteraction("inventorySelectMenu")]
    public async Task ItemShowcase(string[] selections)
    {
        var itemId = Int32.Parse(selections.First());
        CurrentItemId = itemId;
        var item = Items.GetItems()[itemId];
        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new ItemShowcaseEmbed(item).Build();
            message.Components = new ItemShowcaseComponent().Build();
        });
    }
    [ComponentInteraction("inventoryBackButton")]
    public async Task InventoryBack()
    {
        var items = Items.GetItems();

        var playerItems = _database.Inventory
            .Where(i => i.UserId == Context.User.Id &&
                        i.GuildId == Context.Guild.Id &&
                        i.Amount != 0)
            .Select(i => new { i.ItemId, i.Amount })
            .ToDictionary(i => items[i.ItemId], i => i.Amount);

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new InventoryEmbed(playerItems).Build();
            message.Components = new InventoryComponent(playerItems).Build();
        });
    }
    [ComponentInteraction("dropItemButton")]
    public async Task DropItem()
    {
        _database.Inventory.Where(
        i => i.UserId == Context.User.Id &&
             i.GuildId == Context.Guild.Id &&
             i.ItemId == CurrentItemId
        ).First().Amount -= 1;
        await _database.SaveChangesAsync();
        await DeferAsync();
        await Context.Interaction.DeleteOriginalResponseAsync();
    }
}
