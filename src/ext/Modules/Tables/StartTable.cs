using Discord.Interactions;
using Microsoft.Extensions.Logging;
using RPGBot.Database;
using RPGBot.Components;
using RPGBot.Components.Embeds;
using Discord;

namespace RPGBot.Modules;

public partial class GameModule
{
    private static readonly EmbedBuilder _classChoiceEmbed = new ClassChoiceEmbed();
    private static readonly EmbedBuilder _presentChoiceEmbed = new PresentChoiceEmbed();
    private static readonly ComponentBuilder _classChoiceComponents = new ClassChoiceComponents();
    private static readonly ComponentBuilder _presentChoiceComponents = new PresentChoiceComponents();

    [ComponentInteraction("classSelectMenu")]
    public async Task ClassChoiceHandler(string[] selections)
    {
        await SetClass(selections);

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message => {
            message.Embed = _presentChoiceEmbed.Build();
            message.Components = _presentChoiceComponents.Build();
        });
    }
    [ComponentInteraction("presentSelectMenu")]
    public async Task PresentChoiceHandler(string[] selections)
    {
        await SetPresent(selections);

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message => {
            message.Embed = _mainTableEmbed.Build();
            message.Components = _mainTableComponents.Build();
        });
    }
    private async Task SetClass(string[] selections)
    {
        var player = await GetOrCreatePlayer();
        player.ClassId = int.Parse(selections.First());
        _database.SaveChanges();
    }
    private async Task SetPresent(string[] selections)
    {
        var player = await GetOrCreatePlayer();
        player.PresentId = int.Parse(selections.First());
        player.IsStarted = true;
        _database.SaveChanges();
    }
}
