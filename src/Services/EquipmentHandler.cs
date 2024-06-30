using RPGBot.Database;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RPGBot.Database.Models;
using RPGBot.Data;
using Discord;
using System.Numerics;
using System.Linq;

namespace RPGBot.Services;

public class EquipmentHandler
{
    private readonly ILogger _logger;
    private readonly RPGBotEntities _database;
    
    public EquipmentHandler(
        ILogger<StartupService> logger,
        RPGBotEntities database 
    )
    {
        _logger = logger;
        _database = database;  
    }
    public async Task<Dictionary<int, Item>> GetPlayerEquipment(Player player)
    {
        var items = Items.GetItems();
        var playerEquipment = await _database.Equipment.Where(i =>
                i.GuildId == player.GuildId &&
                i.UserId == player.UserId &&
                i.ItemId != 0)
            .Select(i => new { i.ItemId, i.Slot })
            .OrderBy(i => i.Slot).Take(25)
            .ToDictionaryAsync(i => i.Slot, i => items[i.ItemId]);

        return playerEquipment;
    }
    public async Task RemoveItem(Player player, int? itemId)
    {
        if (itemId == null) return;

        _database.Equipment.Where(
        i => i.UserId == player.UserId &&
             i.GuildId == player.GuildId &&
             i.ItemId == itemId
        ).First().ItemId = 0;
        await _database.SaveChangesAsync();
    }
    public async Task EquipItem(Player player, int itemId, int slot)
    {
        if (itemId == null) return;

        _database.Equipment.Where(
            i => i.GuildId == player.GuildId &&
                 i.UserId == player.UserId &&
                 i.Slot == slot
            ).First().ItemId = itemId;

        await _database.SaveChangesAsync();
    }
}