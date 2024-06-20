using Discord.Interactions;
using Microsoft.EntityFrameworkCore;

using RPGBot.UserInterface.Embeds;
using RPGBot.UserInterface;
using RPGBot.Database.Models;
using RPGBot.Data;
using RPGBot.Modules.Game.Services;

namespace RPGBot.Modules.Game;

public class NotesTable(IServiceProvider services) : BaseModule(services)
{
    private static int CurrentQuestId { get; set; }

    [ComponentInteraction("activeQuestSelectMenu")]
    public async Task QuestShowcase(string[] selections)
    {
        var player = await GetOrCreatePlayerAsync();
        var questId = int.Parse(selections.First());
        var questInfo = await _questBoard.GetQuestInfo(
            player, questId
        );
        CurrentQuestId = questId;
        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new NotesShowcaseEmbed(questInfo).Build();
            message.Components = new NotesShowcaseComponent().Build();
        });
    }

    [ComponentInteraction("backToNotesButton")]
    public async Task NotesBackButton()
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
}
