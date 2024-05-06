using Discord;
using Discord.Interactions;
using RPGBot.Components;
using RPGBot.Components.Embeds;
using RPGBot.Database;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

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
            await FollowupAsync(embed: new InventoryEmbed(items).Build(), ephemeral: true);
        }
    }
}
