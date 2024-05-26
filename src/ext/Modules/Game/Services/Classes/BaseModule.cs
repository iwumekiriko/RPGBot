using Discord;
using Discord.Interactions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using RPGBot.Services;
using RPGBot.Database.Models;
using RPGBot.Database;
using RPGBot.UserInterface.Embeds;
using RPGBot.UserInterface;
using RPGBot.Data;


namespace RPGBot.Modules.Game.Services;

public class BaseModule : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService GameModule { get; set; }

    public readonly InteractionHandler _handler;
    public readonly ILogger _logger;
    public readonly RPGBotEntities _database;
    public readonly ImagesHandler _images;
    //public readonly InventoryHandler _inventory;

    public static readonly EmbedBuilder mainEmbed = new MainTableEmbed();
    public static readonly ComponentBuilder mainComponents = new MainTableComponents();

    public BaseModule(IServiceProvider services)
    {
        _handler = services.GetRequiredService<InteractionHandler>();
        _logger = services.GetRequiredService<ILogger<InteractionHandler>>();
        _database = services.GetRequiredService<RPGBotEntities>();
        _images = services.GetRequiredService<ImagesHandler>();
        //_inventory = services.GetRequiredService<InventoryHandler>();
    }
    public async Task<Player> GetOrCreatePlayerAsync()
    {
        var guild = await _database.Guilds.FindAsync(Context.Guild.Id) ?? new Guild { Id = Context.Guild.Id };
        var user = await _database.Users.FindAsync(Context.User.Id) ?? new User { Id = Context.User.Id };
        var player = await _database.Players.FirstOrDefaultAsync(p => p.Guild == guild && p.User == user);

        if (player == null)
        {
            player = new Player { Guild = guild, User = user };
            _database.Add(player);

            await CreateUserData(player.Guild, player.User);
            _database.SaveChanges();

            _logger.LogInformation("New player with id: {playerId} was added to database", user.Id);
        }

        return player;
    }
    private Task CreateUserData(Guild guild, User user)
    {
        AddUsersInventory(guild, user);
        AddUserQuests(guild, user);

        return Task.CompletedTask;
    }
    private Task AddUsersInventory(Guild guild, User user)
    {
        var items = Items.GetItems();

        foreach (var item in items)
        {
            var inventoryItem = new Inventory
            {
                User = user,
                Guild = guild,
                ItemId = item.Key
            };
            _database.Inventory.Add(inventoryItem);
        }

        return Task.CompletedTask;
    }
    private Task AddUserQuests(Guild guild, User user)
    {
        var quests = Quests.GetQuests();

        foreach (var quest in quests)
        {
            var userQuest = new QuestBoardItem
            {
                User = user,
                Guild = guild,
                QuestId = quest.Key
            };
            _database.QuestBoard.Add(userQuest);
        }

        return Task.CompletedTask;
    }
}
