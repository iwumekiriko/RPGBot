using Discord.Interactions;
using RPGBot.Database;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RPGBot.Services;
using Discord;
using RPGBot.Components.Embeds;
using RPGBot.Components;


namespace RPGBot.Modules.Game.Services;

public class BaseModule : InteractionModuleBase<SocketInteractionContext>
{
    public InteractionService GameModule { get; set; }

    public readonly InteractionHandler _handler;
    public readonly ILogger _logger;
    public readonly RPGBotEntities _database;
    public readonly InventoryHandler _inventory;

    public static readonly EmbedBuilder mainEmbed = new MainTableEmbed();
    public static readonly ComponentBuilder mainComponents = new MainTableComponents();

    public BaseModule(IServiceProvider services)
    {
        _handler = services.GetRequiredService<InteractionHandler>();
        _logger = services.GetRequiredService<ILogger<InteractionHandler>>();
        _database = services.GetRequiredService<RPGBotEntities>();
        _inventory = services.GetRequiredService<InventoryHandler>();
    }
}
