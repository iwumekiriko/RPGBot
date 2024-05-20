using Discord.Interactions;
using RPGBot.UserInterface;
using RPGBot.UserInterface.Embeds;

namespace RPGBot.Modules.Game;

public partial class GameModule
{
    private static int CurrentItemId { get; set; }

    [ComponentInteraction("inventorySelectMenu")]
    public async Task ItemShowcase(string[] selections)
    {
        var itemId = Int32.Parse(selections.First());
        CurrentItemId = itemId;
        var item = await _inventory.GetItemByIdAsync(itemId);
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
        var items = _inventory
            .GetPlayerInventory(Context.Guild.Id, Context.User.Id);

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new InventoryEmbed(items).Build();
            message.Components = new InventoryComponent(items).Build();
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
