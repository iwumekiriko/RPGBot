using Discord.Interactions;
using Microsoft.EntityFrameworkCore;

using RPGBot.UserInterface.Embeds;
using RPGBot.UserInterface;
using RPGBot.Database.Models;
using RPGBot.Data;
using RPGBot.Modules.Game.Services;
using System.Numerics;

namespace RPGBot.Modules.Game;

public class JournalTable(IServiceProvider services) : BaseModule(services)
{
    [ComponentInteraction("notesButton")]
    public async Task NotesHandler()
    {
        var player = await GetOrCreatePlayerAsync();
        var playerQuests = await _questBoard.GetStartedPlayerQuests(player);
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new NotesEmbed(playerQuests).Build();
            message.Components = new NotesComponent(playerQuests).Build();
        });
    }

    [ComponentInteraction("archivesButton")]
    public async Task ArchivesHandler()
    {
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new ArchivesEmbed().Build();
            message.Components = new ArchivesComponent().Build();
        });
    }

    [ComponentInteraction("mailButton")]
    public async Task MailHandler()
    {
        await DeferAsync();
        await FollowupAsync(
            "Mail",
            ephemeral: true
        );
    }

    [ComponentInteraction("mapButton")]
    public async Task MapHandler()
    {
        await DeferAsync();
        await FollowupAsync(
            "Map",
            ephemeral: true
        );
    }

    [ComponentInteraction("backToJournalButton")]
    public async Task NotesBackToJournal()
    {
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Content = null;
            message.Embed = new JournalEmbed().Build();
            message.Components = new JournalComponent().Build();
        });
    }
}
