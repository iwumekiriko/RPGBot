using Discord.Interactions;

using RPGBot.Modules.Game.Services;

namespace RPGBot.Modules.Game;

public class JourneyTable(IServiceProvider services) : BaseModule(services)
{
    [ComponentInteraction("exploreButton")]
    public async Task exploreHandler()
    {

    }

    [ComponentInteraction("arenaButton")]
    public async Task ArenaHandler()
    {

    }
}
