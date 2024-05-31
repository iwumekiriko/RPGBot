using RPGBot.Database;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RPGBot.Database.Models;
using RPGBot.Data;
using Discord;
using System.Numerics;

namespace RPGBot.Services;

public class QuestBoardHandler
{
    private readonly ILogger _logger;
    private readonly RPGBotEntities _database;
    private readonly InventoryHandler _inventory;

    public QuestBoardHandler(
        ILogger<StartupService> logger,
        RPGBotEntities database,
        InventoryHandler inventory
    )
    {
        _logger = logger;
        _database = database;
        _inventory = inventory;
    }
    /// <summary>
    /// Gets all not finished player's quests
    /// </summary>
    /// <param name="player">current user</param>
    /// <returns>List of quests (Quest)</returns>
    public async Task<List<Quest>> GetPlayerQuests(Player player)
    {
        var quests = Quests.GetQuests();
        return await _database.QuestBoard.Where(q =>
                q.GuildId == player.GuildId &&
                q.UserId == player.UserId &&
                !q.IsFinished)
            .Select(q => quests[q.QuestId])
            .ToListAsync();
    }
    public async Task<List<Quest>> GetActivePlayerQuests(Player player)
    {
        var quests = Quests.GetQuests();
        return await _database.QuestBoard.Where(q =>
                q.GuildId == player.GuildId &&
                q.UserId == player.UserId &&
                q.IsStarted)
            .Select(q => quests[q.QuestId])
            .ToListAsync();
    }
    /// <summary>
    /// Shortcut for quest selection
    /// </summary>
    /// <param name="questId">quest's id</param>
    /// <returns>Quest by id or null if there is no such id in data</returns>
    public Quest? GetQuest(int questId)
        => Quests.GetQuests()[questId] ?? null;
    /// <summary>
    /// Compares data from database with local data to make QuestInfo
    /// </summary>
    /// <param name="player">current user</param>
    /// <param name="questId">quest's id</param>
    /// <returns>Pair of quest (Quest) and tuple with info about it (Progress, isStarted, isFinished)</returns>
    public async Task<KeyValuePair<Quest, Tuple<int, bool, bool>>> GetQuestInfo(
        Player player, int questId
    )
    {
        var quests = Quests.GetQuests();
        return await _database.QuestBoard.Where(q =>
                q.GuildId == player.GuildId &&
                q.UserId == player.UserId &&
                q.QuestId == questId)
            .Select(q => new KeyValuePair<Quest, Tuple<int, bool, bool>>
            (
                quests[q.QuestId],
                new Tuple<int, bool, bool>(
                    q.Progress, q.IsStarted, q.IsFinished
                )
            )).FirstAsync();
    }
    /// <summary>
    /// Changes isStarted variable in database to true
    /// </summary>
    /// <param name="player">current user</param>
    /// <param name="questId">quest's id</param>
    public void StartQuest(Player player, int questId)
    {
        _database.QuestBoard.Where(q =>
                q.GuildId == player.GuildId &&
                q.UserId == player.UserId &&
                q.QuestId == questId)
            .First().IsStarted = true;
        _database.SaveChanges();
    }
    /// <summary>
    /// Changes isFinished variable in database to true
    /// </summary>
    /// <param name="player">current user</param>
    /// <param name="questId">quest's id</param>
    public void FinishQuest(Player player, int questId)
    {
        _database.QuestBoard.Where(q =>
                q.GuildId == player.GuildId &&
                q.UserId == player.UserId &&
                q.QuestId == questId)
            .First().IsFinished = true;
        _database.SaveChanges();
    }
    /// <summary>
    /// Completes Quest and awards player if the conditions are met
    /// </summary>
    /// <param name="player">current user</param>
    /// <param name="questId">quest's id</param>
    /// <returns>true if conditions are met and false if not</returns>
    public async Task<bool> AcceptFinishQuest(Player player, int questId)
    {
        var questInfo = await GetQuestInfo(
            player, questId
        );
        var quest = questInfo.Key;
        if (questInfo.Value.Item1 >= quest.NeededToComplete)
        {
            if (quest.ItemId != null)
            {
                _inventory.AddItemToInventory(
                    player, quest.ItemId
                );
            }
            if (quest.MoneyReward > 0)
            {
                player.Money += quest.MoneyReward;
            }
            player.Experience += quest.ExpReward;
            FinishQuest(player, questId );
            await _database.SaveChangesAsync();
            return true;
        }
        else
            return false;
    }
}