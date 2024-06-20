using Discord.Interactions;
using RPGBot.UserInterface.Modals;
using RPGBot.Modules.Game.Services;

namespace RPGBot.Modules.Game;

public class ArchivesShowcaseTable(IServiceProvider services) : BaseModule(services)
{
    [ComponentInteraction("pageBackButton")]
    public async Task PageBackButton()
    {
        await DeferAsync();
        await FollowupAsync(
            "Page Back",
            ephemeral: true
        );
    }

    [ComponentInteraction("pageAheadButton")]
    public async Task PageAheadButton()
    {
        await DeferAsync();
        await FollowupAsync(
            "Page Ahead",
            ephemeral: true
        );
    }
    
    [ComponentInteraction("searchButton")]
    public async Task SearchButton()
    {
        await RespondWithModalAsync(new SearchModal().Build());
    }
}
