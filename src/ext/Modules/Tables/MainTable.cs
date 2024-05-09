using Discord;
using Discord.Interactions;
using RPGBot.Components;
using RPGBot.Components.Embeds;

namespace RPGBot.Modules
{
    public partial class GameModule
    {
        private static readonly EmbedBuilder _mainTableEmbed = new MainTableEmbed();
        private static readonly ComponentBuilder _mainTableComponents = new MainTableComponents();

        [ComponentInteraction("inventoryButton")]
        public async Task InventoryHandler()
        {
            var items = _database.Inventory
                .Where(i => i.UserId == Context.User.Id &&
                            i.GuildId == Context.Guild.Id &&
                            i.Amount != 0)
                .Select(i => new {i.Item, i.Amount})
                .ToDictionary(i => i.Item, i => i.Amount);
            
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
            var item = await _inventory.GetItemByIdAsync(Int32.Parse(selections.First()));

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
            var items = _database.Inventory
                .Where(i => i.UserId == Context.User.Id &&
                            i.GuildId == Context.Guild.Id &&
                            i.Amount != 0)
                .Select(i => new { i.Item, i.Amount })
                .ToDictionary(i => i.Item, i => i.Amount);

            await Context.Interaction.DeferAsync();
            await ModifyOriginalResponseAsync(message =>
            {
                message.Embed = new InventoryEmbed(items).Build();
                message.Components = new InventoryComponents(items).Build();
            });
        }
    }
}
