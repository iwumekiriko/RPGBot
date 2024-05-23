using Discord.Interactions;
using RPGBot.Database.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RPGBot.Services;
using Discord;
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
    //public readonly InventoryHandler _inventory;

    public static readonly EmbedBuilder mainEmbed = new MainTableEmbed();
    public static readonly ComponentBuilder mainComponents = new MainTableComponents();

    public BaseModule(IServiceProvider services)
    {
        _handler = services.GetRequiredService<InteractionHandler>();
        _logger = services.GetRequiredService<ILogger<InteractionHandler>>();
        _database = services.GetRequiredService<RPGBotEntities>();
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

            await CreateUserDataAsync(player.Guild, player.User);
            await _database.SaveChangesAsync();

            _logger.LogInformation("New player with id: {playerId} was added to database", user.Id);
        }

        return player;
    }
    private async Task CreateUserDataAsync(Guild guild, User user)
    {
        //await AddUsersInventoryAsync(guild, user);
        await AddUserQuestsAsync(guild, user);
    }
    private async Task AddUsersInventoryAsync(Guild guild, User user)
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
            await _database.Inventory.AddAsync(inventoryItem);
        }
    }
    private async Task AddUserQuestsAsync(Guild guild, User user)
    {
        var quests = await _database.Quests.ToListAsync();

        foreach (var quest in quests)
        {
            var userQuest = new QuestBoardItem
            {
                User = user,
                Guild = guild,
                QuestId = quest.Id
            };
            await _database.QuestBoard.AddAsync(userQuest);
        }
    }
}
