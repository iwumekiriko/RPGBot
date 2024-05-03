using Discord;
using Discord.Interactions;
using RPGBot.Database;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using RPGBot.Data;
using Microsoft.EntityFrameworkCore;

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

        PrepareData();
    }
    [SlashCommand("rpg", "the beginning of the end")]
    public async Task RolePlayGame()
    {
        var player = await GetOrCreatePlayer();
        var embed = player.IsStarted ? _mainEmbed : _classChoiceEmbed;
        var components = player.IsStarted ? _mainComponents : _classChoiceComponents;

        await RespondAsync(
            embed: embed.Build(),
            components: components.Build()
        );
    }
    private async Task<Players> GetOrCreatePlayer()
    {
        var guild = await _database.Guilds.FindAsync(Context.Guild.Id) ?? new Guilds { Id = Context.Guild.Id };
        var user = await _database.Users.FindAsync(Context.User.Id) ?? new Users { Id = Context.User.Id };
        var player = await _database.Players.FirstOrDefaultAsync(p => p.Guild == guild && p.User == user);

        if (player == null)
        {
            player = new Players { Guild = guild, User = user };
            _database.Add(player);
            _logger.LogInformation("New player with id: {playerId} was added to database", user.Id);
        }
        _database.SaveChanges();
        return player;
    }
    private Task PrepareData()
    {
        foreach (var gameClass in Enum.GetValues(typeof(Classes))) 
            _classSelectMenu.AddOption(
                label: gameClass.ToString(),
                value: ((int)gameClass).ToString());

        foreach (var gameRace in Enum.GetValues(typeof(MomsPresent)))
            _presentSelectMenu.AddOption(
                label: gameRace.ToString(),
                value: ((int)gameRace).ToString());

        _classChoiceComponents.WithSelectMenu(_classSelectMenu);
        _presentChoiceComponents.WithSelectMenu(_presentSelectMenu);
        _mainButtons.ToList().ForEach(button => _mainComponents.WithButton(button));

        return Task.CompletedTask;
    }

}