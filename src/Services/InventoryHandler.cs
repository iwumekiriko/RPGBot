using RPGBot.Database;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RPGBot.Database.Models;

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
    public async void AddItemToInventory(ulong guildId, ulong userId, int itemId)
    {
        _database.Inventory.Where(
            i => i.GuildId == guildId &&
                 i.UserId == userId &&
                 i.ItemId == itemId
            ).First().Amount += 1;

        await _database.SaveChangesAsync();
    }
}