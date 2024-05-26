using Discord;
using Discord.WebSocket;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using RPGBot.Database;
using RPGBot.Database.Models;
using RPGBot.Utils.Paths;

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
        var token = _config["token"];
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new Exception("Token missing from environment variables!");
        }
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        if (RPGBot.IsDevelopment())
            await PrepareAsync();
    }
    private async Task PrepareAsync()
    {
        var settings = _config.GetSection("parameters");
        if (settings.GetValue<bool>("recreateDatabase"))
            await RecreateDatabase();
        if (settings.GetValue<bool>("prepareDatabase"))
            await PrepareDatabase();
        if (settings.GetValue<bool>("cacheImages"))
            await UploadImages();
        //await _database.Database.MigrateAsync();
    }
    private async Task PrepareDatabase()
    {
        //await _database.Items.AddRangeAsync(Items.GetItems());
        //await _database.Quests.AddRangeAsync(Quests.GetQuests());
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
    public async Task UploadImages()
    {
        var clientId = _config["imgurApi:clientId"];
        var apiClient = new ApiClient(clientId);

        var filePaths = PathHelper.GetAllPhotos();
        foreach (var filePath in filePaths) 
        {
            var httpClient = new HttpClient();
            using var fileStream = File.OpenRead(filePath);
            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
            var imageUpload = await imageEndpoint.UploadImageAsync(fileStream);
            var uploadedImage = new ImageCache() 
            { 
                ImageName = Path.GetFileName(filePath),
                ImageUrl = imageUpload.Link 
            };
            _database.ImageCaches.Add(uploadedImage);
        }
        await _database.SaveChangesAsync();
        _logger.LogInformation(": Photos uploaded");
    }

}