using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;
using Serilog;
using RPGBot.Database;
using RPGBot.Services;

namespace RPGBot;

public class RPGBot
{
    private static IConfiguration _configuration;

    private static readonly DiscordSocketConfig _socketConfig = new()
    {
        GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.GuildMembers,
        AlwaysDownloadUsers = true,
    };

    private static readonly InteractionServiceConfig _interactionServiceConfig = new()
    {
        LocalizationManager = new ResxLocalizationManager("RPGBot.Resources.CommandLocales", Assembly.GetEntryAssembly(),
            new CultureInfo("en-US"))
    };

    private static async Task Main(string[] args)
    {
        _configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables(prefix: "rpg_")
            .AddJsonFile("appsettings.json")
            .Build();

        var services = new ServiceCollection()
            .AddSingleton(_configuration)
            .AddSingleton(_socketConfig)
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>(), _interactionServiceConfig))
            .AddSingleton<InteractionHandler>()
            .AddSingleton<StartupService>()
            .AddSingleton<InventoryHandler>()
            .AddSingleton<LoggingService>();

        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetRequiredService<LoggingService>();
        await serviceProvider.GetRequiredService<InteractionHandler>()
            .InitializeAsync();
        await serviceProvider.GetRequiredService<StartupService>()
            .StartAsync();

        await Task.Delay(Timeout.Infinite);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<RPGBotEntities>(options => options
            .UseNpgsql(_configuration["connectionStrings:postgresConnection"]));
            //.EnableSensitiveDataLogging(true));
        services.AddLogging(configure => configure.AddSerilog());
        services.RemoveAll<IHttpMessageHandlerBuilderFilter>();
        var logLevel = _configuration["logLevel"];
        var level = Serilog.Events.LogEventLevel.Error;
        if (!string.IsNullOrEmpty(logLevel))
        {
            switch (logLevel.ToLower())
            {
                case "error":
                    {
                        level = Serilog.Events.LogEventLevel.Error;
                        break;
                    }
                case "info":
                    {
                        level = Serilog.Events.LogEventLevel.Information;
                        break;
                    }
                case "debug":
                    {
                        level = Serilog.Events.LogEventLevel.Debug;
                        break;
                    }
                case "critical":
                    {
                        level = Serilog.Events.LogEventLevel.Fatal;
                        break;
                    }
                case "warn":
                    {
                        level = Serilog.Events.LogEventLevel.Warning;
                        break;
                    }
            }
        }
        Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/rpg_bot.log", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .MinimumLevel.Is(level)
                .CreateLogger();
    }
    public static bool IsDebug()
    {
#if DEBUG
        return true;
#else
        return false;
#endif
    }
}
