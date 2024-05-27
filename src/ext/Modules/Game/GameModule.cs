using Discord.Interactions;
using Discord;
using RPGBot.UserInterface.Embeds;
using RPGBot.UserInterface;
using RPGBot.Utils.Embeds;
using RPGBot.Modules.Game.Services;

namespace RPGBot.Modules.Game;

public partial class GameModule(IServiceProvider services) : BaseModule(services) 
{
    [SlashCommand("rpg", "The beginning of the end")]
    public async Task RolePlayGame()
    {
        await RespondAsync(
            "Loading data, please wait <a:loading:1244113411659530294>"
        );

        var player = await GetOrCreatePlayerAsync();
        var (photoName, components) = player.StartPhase switch
        {
            0 => ("Welcome.png", new WelcomeComponent()),
            1 => ("Class.png", new ClassChoiceComponent()),
            2 => ("Present.png", new PresentChoiceComponent()),
            3 => ("Welcome.png", mainComponents), //Main.png
            _ => throw new InvalidDataException()
        };
        var imageUrl = await _images.GetImageUrlAsync(photoName);
        await ModifyOriginalResponseAsync(message =>
        {
            message.Content = null;
            message.Embed = new OnlyImageEmbed(imageUrl).Build();
            message.Components = components.Build();
        });
    }
}