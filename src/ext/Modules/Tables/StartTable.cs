using Discord.Interactions;
using Microsoft.Extensions.Logging;
using RPGBot.Database;
using RPGBot.Components;
using RPGBot.Components.Embeds;
using Discord;
using Microsoft.EntityFrameworkCore;

namespace RPGBot.Modules;

public partial class GameModule
{
    private static readonly EmbedBuilder _classChoiceEmbed = new ClassChoiceEmbed();
    private static readonly EmbedBuilder _presentChoiceEmbed = new PresentChoiceEmbed();
    private static readonly ComponentBuilder _classChoiceComponents = new ClassChoiceComponents();
    private static readonly ComponentBuilder _presentChoiceComponents = new PresentChoiceComponents();

    [ComponentInteraction("classSelectMenu")]
    public async Task ClassChoiceHandler(string[] selections)
    {
        await SetClass(selections);

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message => {
            message.Embed = _presentChoiceEmbed.Build();
            message.Components = _presentChoiceComponents.Build();
        });
    }
    [ComponentInteraction("presentSelectMenu")]
    public async Task PresentChoiceHandler(string[] selections)
    {
        await AddPresent(selections);

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message => {
            message.Embed = _mainTableEmbed.Build();
            message.Components = _mainTableComponents.Build();
        });
    }
    private async Task SetClass(string[] selections)
    {
        var guild = await _database.Guilds.FindAsync(Context.Guild.Id) ?? new Guild { Id = Context.Guild.Id };
        var user = await _database.Users.FindAsync(Context.User.Id) ?? new User { Id = Context.User.Id };
        var player = await _database.Players.FirstOrDefaultAsync(p => p.Guild == guild && p.User == user);
        if (player != null) return;

        player = selections.First() switch
        {
            "101" => new Warrior { Guild = guild, User = user },
            "102" => new Hunter { Guild = guild, User = user },
            "103" => new Mage { Guild = guild, User = user },
            _ => throw new InvalidDataException()
        };

        await _database.AddAsync(player);
        await AddUsersInventoryAsync();
        await _database.SaveChangesAsync();
        _logger.LogInformation("New player with id: {playerId} was added to database", user.Id);
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
                 i.ItemId == Int32.Parse(selections.First())
            ).First().Amount += 1;
        await _database.SaveChangesAsync();
    }
    private async Task<Player?> GetPlayerIfExists()
    {
        var guild = await _database.Guilds.FindAsync(Context.Guild.Id) ?? new Guild { Id = Context.Guild.Id };
        var user = await _database.Users.FindAsync(Context.User.Id) ?? new User { Id = Context.User.Id };
        var player = await _database.Players.FirstOrDefaultAsync(p => p.Guild == guild && p.User == user);
        return player;
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
                ItemId = item.Id,
                Amount = 0
            };
            await _database.Inventory.AddAsync(inventoryItem);
        }
        await _database.SaveChangesAsync();
    }
}
