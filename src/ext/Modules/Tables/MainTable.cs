using Discord;
using Discord.Interactions;
using Microsoft.Extensions.Logging;
using RPGBot.Components;
using RPGBot.Components.Embeds;
using RPGBot.Database;

namespace RPGBot.Modules
{
    public partial class GameModule
    {
        private static readonly EmbedBuilder _mainTableEmbed = new MainTableEmbed();
        private static readonly ComponentBuilder _mainTableComponents = new MainTableComponents();

        private static int CurrentItemId { get; set; } = 0;

        [ComponentInteraction("inventoryButton")]
        public async Task InventoryHandler()
        {
            var items = _inventory
                .GetPlayerInventory(Context.Guild.Id, Context.User.Id);

            await Context.Interaction.DeferAsync();
            await FollowupAsync(
                embed: new InventoryEmbed(items).Build(),
                components: new InventoryComponents(items).Build(),
                ephemeral: true
            );
        }
        [ComponentInteraction("inventorySelectMenu")]
        public async Task ItemShowcase(string[] selections)
        {
            var itemId = Int32.Parse(selections.First());
            CurrentItemId = itemId;
            _logger.LogInformation("{id}", CurrentItemId);
            _logger.LogInformation("{id}", itemId);
            var item = await _inventory.GetItemByIdAsync(itemId);
            await Context.Interaction.DeferAsync();
            await ModifyOriginalResponseAsync(message =>
                {
                    message.Embed = new ItemShowcaseEmbed(item).Build();
                    message.Components = new ItemShowcaseComponents().Build();
                });
        }
        [ComponentInteraction("inventoryBackButton")]
        public async Task InventoryBack()
        {
            _logger.LogInformation("{id}", CurrentItemId);

            var items = _inventory
                .GetPlayerInventory(Context.Guild.Id, Context.User.Id);

            await Context.Interaction.DeferAsync();
            await ModifyOriginalResponseAsync(message =>
            {
                message.Embed = new InventoryEmbed(items).Build();
                message.Components = new InventoryComponents(items).Build();
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
}
