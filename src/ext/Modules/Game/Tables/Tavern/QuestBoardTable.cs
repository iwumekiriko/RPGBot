﻿using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using RPGBot.UserInterface;
using RPGBot.UserInterface.Embeds;
using RPGBot.Data;
using RPGBot.Database.Models;

namespace RPGBot.Modules.Game;

public partial class GameModule
{
    private static int CurrentQuestId { get; set; }

    [ComponentInteraction("questBoardSelectMenu")]
    public async Task QuestShowcase(string[] selections)
    {
        var questId = Int32.Parse(selections.First());
        CurrentQuestId = questId;
        var isStarted = _database.QuestBoard
            .Where(q => q.UserId == Context.User.Id &&
                        q.GuildId == Context.Guild.Id &&
                        q.QuestId == questId)
            .Select(q => q.IsStarted)
            .First();

        var quest = Quests.GetQuests()[questId];

        await _database.SaveChangesAsync();

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new QuestShowcaseEmbed(quest).Build();
            message.Components = new QuestShowcaseComponent(isStarted).Build();
        });
    }
    [ComponentInteraction("questBoardBackButton")]
    public async Task QuestBoardBack()
    {
        var quests = Quests.GetQuests();

        var playerQuests = _database.QuestBoard
            .Where(i => i.UserId == Context.User.Id &&
                        i.GuildId == Context.Guild.Id &&
                        !i.IsFinished)
            .Select(i => new { i.QuestId, i.IsStarted })
            .ToDictionary(i => quests[i.QuestId], i => i.IsStarted);

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new QuestBoardEmbed(playerQuests).Build();
            message.Components = new QuestBoardComponent(playerQuests).Build();
        });
    }
    [ComponentInteraction("takeQuestButton")]
    public async Task TakeQuest()
    {
        var quest = Quests.GetQuests()[CurrentQuestId] ;
        if (quest == null) return;
        
        var questRef = _database.QuestBoard.Where(
            q => q.UserId == Context.User.Id &&
            q.GuildId == Context.Guild.Id &&
            q.QuestId == quest.Id).First();
        questRef.IsStarted = true;
        await _database.SaveChangesAsync();

        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new QuestShowcaseEmbed(quest).Build();
            message.Components = new QuestShowcaseComponent(true).Build();
        });
    }
    [ComponentInteraction("completeQuestButton")]
    public async Task CompleteQuest()
    {
        var quest = Quests.GetQuests()[CurrentQuestId];
        var guild = await _database.Guilds.FindAsync(Context.Guild.Id);
        var user = await _database.Users.FindAsync(Context.User.Id);
        var player = await _database.Players.FirstOrDefaultAsync(p => p.Guild == guild && p.User == user);
        if (guild == null || user == null || player == null || quest == null) return;

        var questRef = _database.QuestBoard
            .Where(q => q.UserId == user.Id &&
                        q.GuildId == guild.Id &&
                        q.QuestId == quest.Id).First();

        if (questRef.Progress >= quest.NeededToComplete)
        {
            if (quest.ItemId != null)
            {
                _database.Inventory.Where
                    (i => i.User == user &&
                     i.Guild == guild &&
                     i.ItemId == quest.ItemId)
                .First().Amount += 1;
            }
            if (quest.MoneyReward > 0)
            {
                player.Money += quest.MoneyReward;
            }
            player.Experience += quest.ExpReward;
            questRef.IsFinished = true;
            await _database.SaveChangesAsync();

            var quests = Quests.GetQuests();

            var playerQuests = _database.QuestBoard
                .Where(i => i.UserId == Context.User.Id &&
                            i.GuildId == Context.Guild.Id &&
                            !i.IsFinished)
                .Select(i => new { i.QuestId, i.IsStarted })
                .ToDictionary(i => quests[i.QuestId], i => i.IsStarted);

            await DeferAsync();
            await ModifyOriginalResponseAsync(message =>
            {
                message.Embed = new QuestBoardEmbed(playerQuests).Build();
                message.Components = new QuestBoardComponent(playerQuests).Build();
            });
        }
        else
        {
            await RespondAsync("Условия квеста не выполнены", ephemeral: true);
        }
    }
}
