using RPGBot.Database;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RPGBot.Database.Models;
using RPGBot.Data;

namespace RPGBot.Services;

public class InventoryHandler
{
    private readonly ILogger _logger;
    private readonly RPGBotEntities _database;

    public InventoryHandler(
        ILogger<StartupService> logger,
        RPGBotEntities database
    )
    {
        _logger = logger;
        _database = database;
    }
    /// <summary>
    /// Sets the amount of item in player's inventory to +1
    /// </summary>
    /// <param name="player">current user</param>
    /// <param name="itemId">item's id</param>
    public async Task AddItemToInventory(Player player, int? itemId)
    {
        if (itemId == null) return;

        _database.Inventory.Where(
            i => i.GuildId == player.GuildId &&
                 i.UserId == player.UserId &&
                 i.ItemId == itemId
            ).First().Amount += 1;

        await _database.SaveChangesAsync();
    }
    /// <summary>
    /// Sets the amount of item in player's inventory to -1
    /// </summary>
    /// <param name="player">current user</param>
    /// <param name="itemId">item's id</param>
    public async Task<bool> DropItemFromInventory(Player player, int itemId, int amount = 1)
    {
        var inventoryItem = await _database.Inventory.FirstOrDefaultAsync(
            i => i.UserId == player.UserId &&
                i.GuildId == player.GuildId &&
                i.ItemId == itemId
        );
        if (inventoryItem == null || inventoryItem.Amount < amount) return false;
        inventoryItem.Amount -= amount;
        await _database.SaveChangesAsync();
        return true;
    }
    /// <summary>
    /// Compares database data with local data and makes Items Dictionary
    /// </summary>
    /// <param name="player"></param>
    /// <returns>Dictionary with:<br/>Keys: Item<br/>Values: Item amount</returns>
    public async Task<Dictionary<Item, int>> GetPlayerItems(Player player)
    {
        var items = Items.GetItems();
        var playerItems = await _database.Inventory.Where(i => 
                i.GuildId == player.GuildId &&
                i.UserId == player.UserId &&
                i.Amount != 0)
            .Select(i => new { i.ItemId, i.Amount })
            .OrderBy(i => i.Amount).Take(25)
            .ToDictionaryAsync(i => items[i.ItemId], i => i.Amount);

        return playerItems;
    }
    /// <summary>
    /// Shortcut for item selection
    /// </summary>
    /// <param name="itemId">item's id</param>
    /// <returns>Item by id or null if there is no such id in data</returns>
    public Item? GetItem(int itemId) 
        => Items.GetItems()[itemId] ?? null;
}