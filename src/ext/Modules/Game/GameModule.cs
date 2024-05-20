using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using RPGBot.Modules.Game.Services;
using RPGBot.Components.Embeds;
using RPGBot.Components;
using RPGBot.Database;

namespace RPGBot.Modules.Game;

public partial class GameModule(IServiceProvider services) : BaseModule(services) 
{
    [SlashCommand("rpg", "The beginning of the end")]
    public async Task RolePlayGame()
    {
        await RespondAsync(
            "Loading data..."
        );

        var player = await GetOrCreatePlayerAsync();
        var phase = player.StartPhase;
        var (embed, components) = phase switch
        {
            0 => (new WelcomeEmbed(), new WelcomeComponents()),
            1 => (new ClassChoiceEmbed(), new ClassChoiceComponents()),
            2 => (new PresentChoiceEmbed(), new PresentChoiceComponents()),
            3 => (mainEmbed, mainComponents),
            _ => throw new InvalidDataException()
        };

        await ModifyOriginalResponseAsync(message =>
        {
            message.Content = null;
            message.Embed = embed.Build();
            message.Components = components.Build();
        });
    }
}