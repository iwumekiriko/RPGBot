using Discord;
using Discord.Interactions;
using RPGBot.Data;
using RPGBot.Attributes;
using RPGBot.Database;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RPGBot.Modules;
    
public class StartModule : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService Commands { get; set; }

    private readonly InteractionHandler _handler;
    private readonly ILogger _logger;
    private readonly RPGBotEntities _database;

    public StartModule(IServiceProvider services)
    {
        _handler = services.GetRequiredService<InteractionHandler>();
        _logger = services.GetRequiredService<ILogger<InteractionHandler>>();
        _database = services.GetRequiredService<RPGBotEntities>();
    }

    [SlashCommand("rpg_start", "the beginning of the end")]
    public async Task Start()
    {
        var embed = new EmbedBuilder
        {
            Title = "Hello",
            Description = "Lets choose your class"
        };

        var select = new SelectMenuBuilder()
            .WithPlaceholder("Select Class")
            .WithCustomId("ClassChoice")
            .WithMinValues(1)
            .WithMaxValues(1)
            .AddOption("Warrior", "war")
            .AddOption("Hunter", "hunt");

        var components = new ComponentBuilder()
            .WithSelectMenu(select);

        Guilds guild = new() { Id = Context.Guild.Id };
        Users user = new() { Id = Context.User.Id };
        await AddPlayer(guild, user);

        await RespondAsync(embed: embed.Build(), components: components.Build());
    }

    private Task AddPlayer(Guilds guild, Users user)
    {
        var db_guild = _database.Find<Guilds>(guild.Id);
        var db_user = _database.Find<Users>(user.Id);

        if (db_guild == null)
        {
            db_guild = new Guilds { Id = guild.Id };
            _database.Add(db_guild);
        }
        if (db_user == null)
        {
            db_user = new Users { Id = user.Id };
            _database.Add(db_user);
        }
        var player = new Players { Guild = db_guild, User = db_user };
        var isAlreadyExists = _database.Find<Players>(player.User.Id, player.Guild.Id);
        if (isAlreadyExists == null)
        {
            _database.Add(player);
            _logger.LogInformation("New player with id: {0} was added", player.User.Id);
        }
        _database.SaveChanges();

        return Task.CompletedTask;
    }


    [ComponentInteraction("ClassChoice")]
    public async Task ClassChoiceHandler(string[] selections)
    {
        await Context.Interaction.DeferAsync();

        if (selections.First() == "war")
        {
            await Context.Interaction.FollowupAsync(selections.First());
        }

        if (selections.First() == "hunt")   
        {
            await Context.Interaction.FollowupAsync(selections.First());
        }

        _logger.LogInformation($"Executed");
    }
}