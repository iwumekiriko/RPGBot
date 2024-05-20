using Discord.Interactions;
using RPGBot.Modules.Game.Services;

namespace RPGBot.Modules.Game;

public class JourneyTable(IServiceProvider services) : BaseModule(services)
{
    [ComponentInteraction("journeyButton")]
    public async Task JourneyHandler()
    {

    }

    [ComponentInteraction("arenaButton")]
    public async Task ArenaHandler()
    {

    }
}
