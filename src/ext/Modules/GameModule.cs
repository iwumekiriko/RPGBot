using Discord;
using Discord.Interactions;
using RPGBot.Database;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

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
        await AddPlayerIfNotExist();

        await RespondAsync(
            embed: _classChoiceEmbed.Build(),
            components: _classComponents.Build()
        );
    }
    private Task AddPlayerIfNotExist()
    {
        var guild = _database.Guilds.FirstOrDefault(g => g.Id == Context.Guild.Id);
        var user = _database.Users.FirstOrDefault(u => u.Id == Context.User.Id);

        if (guild == null)
        {
            guild = new Guilds { Id = Context.Guild.Id };
            _database.Add(guild);
        }
        if (user == null)
        {
            user = new Users { Id = Context.User.Id };
            _database.Add(user);
        }
        var player = _database.Players.FirstOrDefault(
            p => 
            p.Guild.Id == guild.Id &&
            p.User.Id == user.Id
        );
        if (player == null)
        {
            _database.Add(new Players {
                Guild = guild,
                User = user,
            });
            _logger.LogInformation("New player with id: {playerId} was added to database", user.Id);
        }
        _database.SaveChanges();
        return Task.CompletedTask;
    }
    
}