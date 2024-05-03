using Discord;
using Discord.Interactions;
using Microsoft.Extensions.Logging;
using RPGBot.Data;
using RPGBot.Database;
using RPGBot.Utils;

namespace RPGBot.Modules;

public partial class GameModule
{
    private static readonly DefaultEmbed _classChoiceEmbed = new()
    {
        Title = "Hello",
        Description = "Lets choose your class"
    };
    private static readonly DefaultEmbed _raceChoiceEmbed = new()
    {
        Title = "Executed",
        Description = "Exactly"
    };
    private readonly SelectMenuBuilder _classSelectMenu = new SelectMenuBuilder()
        .WithPlaceholder("Select Class")
        .WithCustomId("ClassChoice")
        .WithMinValues(1)
        .WithMaxValues(1);

    private readonly SelectMenuBuilder _raceSelectMenu = new SelectMenuBuilder()
        .WithPlaceholder("Select Race")
        .WithCustomId("RaceChoice")
        .WithMinValues(1)
        .WithMaxValues(1);

    private readonly ComponentBuilder _classComponents = new();
    private readonly ComponentBuilder _raceComponents = new();


    [ComponentInteraction("ClassChoice")]
    public async Task ClassChoiceHandler(string[] selections)
    {
        await Context.Interaction.DeferAsync();

        var player = FindPlayer();
        player.ClassId = int.Parse(selections.First());
        _database.SaveChanges();

        _logger.LogInformation($"Executed1");
        await ModifyOriginalResponseAsync(message => {
            message.Embed = _raceChoiceEmbed.Build();
            message.Components = _raceComponents.Build();
        });
    }
    [ComponentInteraction("RaceChoice")]
    public async Task RaceChoiceHandler(string[] selections)
    {
        await Context.Interaction.DeferAsync();

        var player = FindPlayer();
        player.RaceId = int.Parse(selections.First());
        player.IsStarted = true;
        _database.SaveChanges();

        _logger.LogInformation($"Executed2");
        await ModifyOriginalResponseAsync(message => {
            message.Embed = _raceChoiceEmbed.Build();
            message.Components = null;
        });
    }
    private Players FindPlayer()
    {
        var player = _database.Players.First(
            p =>
            p.UserId == Context.User.Id &&
            p.GuildId == Context.Guild.Id
        );
        return player;
    }
    private Task PrepareData()
    {
        foreach (var gameClass in Enum.GetValues(typeof(Classes)))
        {
            _classSelectMenu.AddOption(gameClass.ToString(), ((int)gameClass).ToString());
        }
        _classComponents.WithSelectMenu(_classSelectMenu);

        foreach (var gameRace in Enum.GetValues(typeof(Races)))
        {
            _raceSelectMenu.AddOption(gameRace.ToString(), ((int)gameRace).ToString());
        }
        _raceComponents.WithSelectMenu(_raceSelectMenu);

        return Task.CompletedTask;
    }
}
