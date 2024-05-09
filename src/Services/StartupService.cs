using Discord;
using Discord.WebSocket;
using RPGBot.Database;
using RPGBot.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RPGBot.Services;

public class StartupService
{
    private readonly DiscordSocketClient _client;
    private readonly IConfiguration _config;
    private readonly ILogger _logger;
    private readonly RPGBotEntities _database;

    public StartupService(
        DiscordSocketClient client,
        IConfiguration config,
        ILogger<StartupService> logger,
        RPGBotEntities database
    )
    {
        _client = client;
        _config = config;
        _logger = logger;
        _database = database;
    }
    public async Task StartAsync()
    {
        var settings = _config.GetSection("parametres");
        var isDevelopment = settings.GetValue<bool>("development");
        if (isDevelopment) 
        {
            if (settings.GetValue<bool>("recreateDatabase"))
                await RecreateDatabase();
            if (settings.GetValue<bool>("prepareDatabase"))
                await PrepareDatabase();
        }

        var token = _config["token"];
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new Exception("Token missing from environment variables!");
        }
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
    }
    private async Task PrepareDatabase()
    {
        await _database.Items.AddRangeAsync(InventoryItems.GetItems());
        await _database.SaveChangesAsync();
        _logger.LogInformation(": Database prepared");
    }
    private Task RecreateDatabase()
    {
        _database.Database.EnsureDeleted();
        _database.Database.EnsureCreated();
        _logger.LogInformation(": Database recreated");

        return Task.CompletedTask;
    }
}