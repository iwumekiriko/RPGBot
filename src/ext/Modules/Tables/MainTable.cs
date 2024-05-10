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
            var items = _inventory
                .GetPlayerInventory(Context.Guild.Id, Context.User.Id);

            await DeferAsync();
            await FollowupAsync(
                embed: new InventoryEmbed(items).Build(),
                components: new InventoryComponents(items).Build(),
                ephemeral: true
            );
        }
        [ComponentInteraction("questBoardButton")]
        public async Task questBoardHandler()
        {
            await DeferAsync();
            await FollowupAsync(
                embed: new QuestBoardEmbed().Build(),
                ephemeral : true
            );
        }
    }
}
