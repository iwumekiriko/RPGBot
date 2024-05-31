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
        LocalizationManager = new ResxLocalizationManager(
            "RPGBot.Resources.Locals.CommandLocales", Assembly.GetEntryAssembly(),
            new CultureInfo("en-US")
        )
    };
    private static async Task Main(string[] args)
    {
        _configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables(prefix: "rpg_")
            .AddJsonFile(
                "appsettings.json",
                optional: false,
                reloadOnChange: true
            ).Build();

        var services = new ServiceCollection()
            .AddSingleton(_configuration)
            .AddSingleton(_socketConfig)
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton(x => new InteractionService(
                x.GetRequiredService<DiscordSocketClient>(),
                _interactionServiceConfig)
            )
            .AddSingleton<InteractionHandler>()
            .AddSingleton<StartupService>()
            .AddSingleton<ImagesHandler>()
            .AddSingleton<InventoryHandler>()
            .AddSingleton<QuestBoardHandler>()
            .AddSingleton<ShopHandler>()
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
            .UseNpgsql(_configuration["connectionStrings:postgresConnection"])
            .EnableSensitiveDataLogging(IsDevelopment()));
        services.AddLogging(configure => configure.AddSerilog());
        services.RemoveAll<IHttpMessageHandlerBuilderFilter>();
        var logLevel = _configuration["parameters:logLevel"];
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
                case "warning":
                    {                               
                        level = Serilog.Events.LogEventLevel.Warning;
                        break;
                    }
            }
        }
        Log.Logger = new LoggerConfiguration()
                .WriteTo.File(path: _configuration["parameters:logFile"],
                              rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .Filter.ByExcluding(l => l.RenderMessage().StartsWith("Executed DbCommand") && // Didn't find how to disable db logs so had to hardcode...
                    l.Level == Serilog.Events.LogEventLevel.Information) 
                .MinimumLevel.Is(level)
                .CreateLogger();
    }
    public static bool IsDevelopment()
    {
        return _configuration.GetValue<bool>("parameters:development");
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
