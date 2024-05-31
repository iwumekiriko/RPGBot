using RPGBot.Database;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using RPGBot.Database.Models;
using RPGBot.Data;
using System.Reflection.Metadata.Ecma335;

namespace RPGBot.Services;

public class ShopHandler
{
    private readonly ILogger _logger;
    private readonly RPGBotEntities _database;
    private readonly InventoryHandler _inventory;

    public ShopHandler(
        ILogger<StartupService> logger,
        RPGBotEntities database,
        InventoryHandler inventory
    )
    {
        _logger = logger;
        _database = database;
        _inventory = inventory;
    }
    private static bool AcceptPay(Player player, int requiredMoney)
    {
        if (player.Money >= requiredMoney)
        {
            player.Money -= requiredMoney;
            return true;
        }
        return false;
    }
    /// <summary>
    /// Checks if player is able to buy an item
    /// <para>Adds bought item to inventory and changes player's balance if true</para>
    /// </summary>
    /// <param name="player">current user</param>
    /// <param name="itemId">item's to buy id</param>
    public async Task<bool> AcceptBuyItem(Player player, int itemId)
    {
        var dbPlayer = _database.Players.First(p =>
            p.GuildId == player.GuildId && p.UserId == player.UserId
        );
        var item = _inventory.GetItem(itemId);
        if ( item == null ) return false;
        if (AcceptPay(dbPlayer, item.Cost))
        {
            await _inventory.AddItemToInventory(player, itemId);
            await _database.SaveChangesAsync();
            return true;
        }
        return false;
    }
}