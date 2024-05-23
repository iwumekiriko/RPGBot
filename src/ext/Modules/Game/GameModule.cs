using Discord.Interactions;

using RPGBot.Modules.Game.Services;
using RPGBot.UserInterface.Embeds;
using RPGBot.UserInterface;

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
            0 => (new WelcomeEmbed(), new WelcomeComponent()),
            1 => (new ClassChoiceEmbed(), new ClassChoiceComponent()),
            2 => (new PresentChoiceEmbed(), new PresentChoiceComponent()),
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