using Discord.Interactions;
using RPGBot.Database;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RPGBot.Services;

namespace RPGBot.Modules;

public partial class GameModule : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService Commands { get; set; }

    private readonly InteractionHandler _handler;
    private readonly ILogger _logger;
    private readonly RPGBotEntities _database;
    private readonly InventoryHandler _inventory;

    public GameModule(IServiceProvider services)
    {
        _handler = services.GetRequiredService<InteractionHandler>();
        _logger = services.GetRequiredService<ILogger<InteractionHandler>>();
        _database = services.GetRequiredService<RPGBotEntities>();
        _inventory = services.GetRequiredService<InventoryHandler>();
    }
    [SlashCommand("rpg", "The beginning of the end")]
    public async Task RolePlayGame()
    {
        var player = await _database.Players
            .FirstOrDefaultAsync(
                p => p.GuildId == Context.Guild.Id &&
                p.UserId == Context.User.Id
            );
        var condition = player != null && player.isStarted;
        var embed = condition ? _mainTableEmbed : _classChoiceEmbed;
        var components = condition ? _mainTableComponents : _classChoiceComponents;

        await RespondAsync(
            embed: embed.Build(),
            components: components.Build()
        );
    }
}