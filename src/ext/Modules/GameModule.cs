using Discord.Interactions;
using RPGBot.Database;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RPGBot.Services;
using Discord;

namespace RPGBot.Modules;

public partial class GameModule : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService Commands { get; set; }

    private readonly InteractionHandler _handler;
    private readonly ILogger _logger;
    private readonly RPGBotEntities _database;

    public GameModule(IServiceProvider services)
    {
        _handler = services.GetRequiredService<InteractionHandler>();
        _logger = services.GetRequiredService<ILogger<InteractionHandler>>();
        _database = services.GetRequiredService<RPGBotEntities>();
    }
    [SlashCommand("rpg", "the beginning of the end")]
    public async Task RolePlayGame()
    {
        var player = await GetOrCreatePlayer();
        var embed = player.IsStarted ? _mainTableEmbed : _classChoiceEmbed;
        var components = player.IsStarted ? _mainTableComponents : _classChoiceComponents;

        await RespondAsync(
            embed: embed.Build(),
            components: components.Build()
        );
    }
    private async Task<Player> GetOrCreatePlayer()
    {
        var guild = await _database.Guilds.FindAsync(Context.Guild.Id) ?? new Guild { Id = Context.Guild.Id };
        var user = await _database.Users.FindAsync(Context.User.Id) ?? new User { Id = Context.User.Id };
        var player = await _database.Players.FirstOrDefaultAsync(p => p.Guild == guild && p.User == user);

        if (player == null)
        {
            player = new Player { Guild = guild, User = user };
            await _database.AddAsync(player);
            await AddUserToInventoryAsync();
            await _database.SaveChangesAsync();
            _logger.LogInformation("New player with id: {playerId} was added to database", user.Id);
        }
        return player;
    }
    private async Task AddUserToInventoryAsync()
    {
        var items = await _database.Items.ToListAsync();

        foreach (var item in items)
        {
            var inventoryItem = new Inventory
            {
                UserId = Context.User.Id,
                GuildId = Context.Guild.Id,
                ItemId = item.Id,
                Amount = Math.Min(new Random().Next(1, 4), item.MaxInStack)
            };

            await _database.Inventory.AddAsync(inventoryItem);
        }

        await _database.SaveChangesAsync();
    }
}