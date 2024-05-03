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
    private static readonly DefaultEmbed _presentChoiceEmbed = new()
    {
        Title = "Executed",
        Description = "Exactly"
    };
    private readonly SelectMenuBuilder _classSelectMenu = new SelectMenuBuilder()
        .WithPlaceholder("Select Class")
        .WithCustomId("classSelectMenu")
        .WithMinValues(1)
        .WithMaxValues(1);

    private readonly SelectMenuBuilder _presentSelectMenu = new SelectMenuBuilder()
        .WithPlaceholder("Select Mom's Present")
        .WithCustomId("presentSelectMenu")
        .WithMinValues(1)
        .WithMaxValues(1);

    private readonly ComponentBuilder _classChoiceComponents = new();
    private readonly ComponentBuilder _presentChoiceComponents = new();


    [ComponentInteraction("classSelectMenu")]
    public async Task ClassChoiceHandler(string[] selections)
    {
        await Context.Interaction.DeferAsync();

        var player = FindPlayer();
        player.ClassId = int.Parse(selections.First());
        _database.SaveChanges();

        _logger.LogInformation($"Executed1");
        await ModifyOriginalResponseAsync(message => {
            message.Embed = _presentChoiceEmbed.Build();
            message.Components = _presentChoiceComponents.Build();
        });
    }
    [ComponentInteraction("presentSelectMenu")]
    public async Task PresentChoiceHandler(string[] selections)
    {
        await Context.Interaction.DeferAsync();

        var player = FindPlayer();
        player.RaceId = int.Parse(selections.First());
        player.IsStarted = true;
        _database.SaveChanges();

        _logger.LogInformation($"Executed2");
        await ModifyOriginalResponseAsync(message => {
            message.Embed = _mainEmbed.Build();
            message.Components = _mainComponents.Build();
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
}
