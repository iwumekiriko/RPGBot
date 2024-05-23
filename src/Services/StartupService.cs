using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using RPGBot.Database;
using RPGBot.Data;

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
        if (RPGBot.IsDevelopment()) 
        {
            var settings = _config.GetSection("parameters");
            if (settings.GetValue<bool>("recreateDatabase"))
                await RecreateDatabase();
            if (settings.GetValue<bool>("prepareDatabase"))
                await PrepareDatabase();
            //await _database.Database.MigrateAsync();
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
        //await _database.Items.AddRangeAsync(Items.GetItems());
        await _database.Quests.AddRangeAsync(Quests.GetQuests());
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