using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using RPGBot.UserInterface;
using RPGBot.UserInterface.Embeds;
using RPGBot.Data;
using RPGBot.Database;

namespace RPGBot.Modules.Game;

public partial class GameModule
{
    private static int CurrentQuestId { get; set; }

    [ComponentInteraction("questBoardSelectMenu")]
    public async Task QuestShowcase(string[] selections)
    {
        var questId = Int32.Parse(selections.First());
        CurrentQuestId = questId;
        var quest = _database.QuestBoard
            .Where(q => q.UserId == Context.User.Id &&
                        q.GuildId == Context.Guild.Id &&
                        q.QuestId == questId)
            .Select(q => new KeyValuePair<Quest, bool>(q.Quest, q.IsStarted))
            .First();

        await _database.SaveChangesAsync();

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new QuestShowcaseEmbed(quest.Key).Build();
            message.Components = new QuestShowcaseComponent(quest.Value).Build();
        });
    }
    [ComponentInteraction("questBoardBackButton")]
    public async Task QuestBoardBack()
    {
        var quests = _database.QuestBoard
            .Where(i => i.UserId == Context.User.Id &&
                        i.GuildId == Context.Guild.Id &&
                        !i.IsFinished)
            .Select(i => new { i.Quest, i.IsStarted })
            .ToDictionary(i => i.Quest, i => i.IsStarted);

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new QuestBoardEmbed(quests).Build();
            message.Components = new QuestBoardComponent(quests).Build();
        });
    }
    [ComponentInteraction("takeQuestButton")]
    public async Task TakeQuest()
    {
        var quest = await _database.Quests.FindAsync(CurrentQuestId);
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
        var quest = await _database.Quests.FindAsync(CurrentQuestId);
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
            if (quest.ItemReward != null)
            {
                _database.Inventory.Where
                    (i => i.User == user &&
                     i.Guild == guild &&
                     i.ItemId == quest.ItemReward.Id)
                .First().Amount += 1;
            }
            if (quest.MoneyReward > 0)
            {
                player.Money += quest.MoneyReward;
            }
            player.Experience += quest.ExpReward;
            questRef.IsFinished = true;
            await _database.SaveChangesAsync();

            var quests = _database.QuestBoard
            .Where(i => i.User == user &&
                        i.Guild == guild &&
                        !i.IsFinished)
            .Select(i => new { i.Quest, i.IsStarted })
            .ToDictionary(i => i.Quest, i => i.IsStarted);

            await DeferAsync();
            await ModifyOriginalResponseAsync(message =>
            {
                message.Embed = new QuestBoardEmbed(quests).Build();
                message.Components = new QuestBoardComponent(quests).Build();
            });
        }
        else
        {
            await RespondAsync("Условия квеста не выполнены", ephemeral: true);
        }
    }
}
