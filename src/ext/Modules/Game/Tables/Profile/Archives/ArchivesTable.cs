using Discord.Interactions;
using RPGBot.UserInterface.Embeds;
using RPGBot.UserInterface;
using RPGBot.Modules.Game.Services;

namespace RPGBot.Modules.Game;

public class ArchivesTable(IServiceProvider services) : BaseModule(services)
{
    private static int CurrentQuestId { get; set; }
    
    [ComponentInteraction("archivesSelectMenu")]
    public async Task archivesSelectMenu(string[] selections)
    {
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new ArchivesShowcaseEmbed(selections.First()).Build();
            message.Components = new ArchivesShowcaseComponent().Build();
        });
    }

    [ComponentInteraction("backToArchivesButton")]
    public async Task ArchivesHandler()
    {
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new ArchivesEmbed().Build();
            message.Components = new ArchivesComponent().Build();
        });
    }
}
