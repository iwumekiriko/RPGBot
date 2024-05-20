using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using RPGBot.Modules.Game.Services;
using RPGBot.Components.Embeds;
using RPGBot.Components;

namespace RPGBot.Modules.Game;

public partial class GameModule(IServiceProvider services) : BaseModule(services) 
{
    [SlashCommand("rpg", "The beginning of the end")]
    public async Task RolePlayGame()
    {
        var player = await _database.Players
            .FirstOrDefaultAsync(
                p => p.GuildId == Context.Guild.Id &&
                p.UserId == Context.User.Id
            );
        var condition = player != null && player.isStarted;
        var embed = condition ? mainEmbed : new WelcomeEmbed();
        var components = condition ? mainComponents : new WelcomeComponents();

        await RespondAsync(
            embed: embed.Build(),
            components: components.Build()
        );
    }

    //TODO хранение предметов/квестов непосредственно в коде, а в бд оставить только айдишки
}