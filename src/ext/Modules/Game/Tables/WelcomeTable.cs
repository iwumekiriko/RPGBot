using Discord.Interactions;
using Microsoft.Extensions.Logging;
using RPGBot.Database;
using RPGBot.Components;
using RPGBot.Components.Embeds;
using Discord;
using Microsoft.EntityFrameworkCore;
using RPGBot.Modules.Game.Services;
using RPGBot.Data;

namespace RPGBot.Modules.Game;

public class WelcomeModule(IServiceProvider services) : BaseModule(services)
{
    private static readonly EmbedBuilder _classesEmbed = new ClassChoiceEmbed();
    private static readonly EmbedBuilder _presentsEmbed = new PresentChoiceEmbed();

    private static readonly ComponentBuilder _classesComponents = new ClassChoiceComponents();
    private static readonly ComponentBuilder _presentsComponents = new PresentChoiceComponents();

    private static int classId;

    [ComponentInteraction("nextButton")]
    public async Task NextHandler()
    {
        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = _classesEmbed.Build();
            message.Components = _classesComponents.Build();
        });
    }
    [ComponentInteraction("creditsButton")]
    public async Task CreditsHandler()
    {
        await Context.Interaction.DeferAsync();
        await FollowupAsync(
            "test",
            ephemeral: true
        );
    }
    [ComponentInteraction("classSelectMenu")]
    public async Task ClassShowcase(string[] selections)
    {
        var id = Convert.ToInt32(selections.First());
        classId = id;

        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new ClassShowcaseEmbed(classId).Build();
        });
    }
    [ComponentInteraction("submitClassButton")]
    public async Task SubmitClassButton()
    {
        await SetClass(classId);

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = _presentsEmbed.Build();
            message.Components = _presentsComponents.Build();
        });
    }

    [ComponentInteraction("presentSelectMenu")]
    public async Task PresentChoiceHandler(string[] selections)
    {
        await AddPresent(selections);

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = mainEmbed.Build();
            message.Components = mainComponents.Build();
        });
    }
    private async Task SetClass(int classId)
    {
        var guild = await _database.Guilds.FindAsync(Context.Guild.Id) ?? new Guild { Id = Context.Guild.Id };
        var user = await _database.Users.FindAsync(Context.User.Id) ?? new User { Id = Context.User.Id };
        var player = await _database.Players.FirstOrDefaultAsync(p => p.Guild == guild && p.User == user);
        if (player != null) return;

        player = classId switch
        {
            101 => new Warrior { Guild = guild, User = user },
            102 => new Hunter { Guild = guild, User = user },
            103 => new Mage { Guild = guild, User = user },
            _ => throw new InvalidDataException()
        };

        await _database.Players.AddAsync(player);
        await CreateUserDataAsync();
        await _database.SaveChangesAsync();
    }
    private async Task AddPresent(string[] selections)
    {
        var guildId = Context.Guild.Id;
        var userId = Context.User.Id;

        var player = await _database.Players
            .FirstOrDefaultAsync(
                p => p.GuildId == guildId &&
                p.UserId == userId
            ) ?? throw new InvalidDataException();
        player.isStarted = true;

        _database.Inventory.Where(
            i => i.UserId == userId &&
                 i.GuildId == guildId &&
                 i.ItemId == int.Parse(selections.First())
            ).First().Amount += 1;

        await _database.SaveChangesAsync();
        _logger.LogInformation("New player with id: {playerId} was added to database", userId);
    }
    private async Task CreateUserDataAsync()
    {
        await AddUsersInventoryAsync();
        await AddUserQuestsAsync();
    }
    private async Task AddUsersInventoryAsync()
    {
        var items = await _inventory.GetItemsAsync();

        foreach (var item in items)
        {
            var inventoryItem = new Inventory
            {
                UserId = Context.User.Id,
                GuildId = Context.Guild.Id,
                ItemId = item.Id
            };
            await _database.Inventory.AddAsync(inventoryItem);
        }
        await _database.SaveChangesAsync();
    }
    private async Task AddUserQuestsAsync()
    {
        var quests = await _database.Quests.ToListAsync();

        foreach (var quest in quests)
        {
            var userQuest = new QuestBoardItem
            {
                UserId = Context.User.Id,
                GuildId = Context.Guild.Id,
                QuestId = quest.Id
            };
            await _database.QuestBoard.AddAsync(userQuest);
        }
        await _database.SaveChangesAsync();
    }
}
