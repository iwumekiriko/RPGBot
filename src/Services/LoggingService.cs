using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace RPGBot.Services
{
    public class LoggingService
    {
        private readonly ILogger _logger;
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _interactions;

        public LoggingService(
            ILogger<LoggingService> logger,
            DiscordSocketClient client,
            InteractionService interactionService
        )
        {
            _client = client;
            _interactions = interactionService;
            _logger = logger;

            _client.Ready += OnReadyAsync;
            _client.Log += OnLogAsync;
            _interactions.Log += OnLogAsync;
        }

        public Task OnReadyAsync()
        {
            _logger.LogInformation($": Bot {_client.CurrentUser}'s ready to start");
            return Task.CompletedTask;
        }

        public Task OnLogAsync(LogMessage msg)
        {
            string logText = $": {msg.Exception?.ToString() ?? msg.Message}";
            switch (msg.Severity.ToString())
            {
                case "Critical":
                    {
                        _logger.LogCritical(logText);
                        break;
                    }
                case "Warning":
                    {
                        _logger.LogWarning(logText);
                        break;
                    }
                case "Info":
                    {
                        _logger.LogInformation(logText);
                        break;
                    }
                case "Verbose":
                    {
                        _logger.LogInformation(logText);
                        break;
                    }
                case "Debug":
                    {
                        _logger.LogDebug(logText);
                        break;
                    }
                case "Error":
                    {
                        _logger.LogError(logText);
                        break;
                    }
            }

            return Task.CompletedTask;

        }
    }
}