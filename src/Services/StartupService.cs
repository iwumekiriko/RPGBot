using Discord;
using Discord.WebSocket;
using RPGBot.Database;
using Microsoft.Extensions.Configuration;

namespace RPGBot.Services
{
    public class StartupService
    {
        private readonly DiscordSocketClient _client;
        private readonly IConfiguration _config;
        private readonly RPGBotEntities _database;

        public StartupService(DiscordSocketClient client, IConfiguration config, RPGBotEntities database)
        {
            _client = client;
            _config = config;
            _database = database;
        }
        public async Task StartAsync()
        {
            _database.Database.EnsureDeleted();
            _database.Database.EnsureCreated();

            var token = _config["token"];
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new Exception("Token missing from environment variables!");
            }
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
        }
    }
}