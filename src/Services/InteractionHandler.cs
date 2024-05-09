using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RPGBot.Services;
public class InteractionHandler
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _handler;
    private readonly IServiceProvider _services;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public InteractionHandler(
        DiscordSocketClient client,
        InteractionService handler,
        IServiceProvider services,
        IConfiguration config
    )
    {
        _client = client;
        _handler = handler;
        _services = services;
        _configuration = config;    
        _logger = services.GetRequiredService<ILogger<InteractionHandler>>();
    }
    public async Task InitializeAsync()
    {
        _client.Ready += ReadyAsync;
        await _handler.AddModulesAsync(System.Reflection.Assembly.GetEntryAssembly(), _services);

        // Process the InteractionCreated payloads to execute Interactions commands
        _client.InteractionCreated += HandleInteraction;

        // Also process the result of the command execution.
        _handler.InteractionExecuted += HandleInteractionExecute;
    }
    private async Task ReadyAsync()
    {
        if (RPGBot.IsDevelopment())
            await _handler.RegisterCommandsToGuildAsync(
                Convert.ToUInt64(_configuration["parameters:testGuild"]), true);
        else
            await _handler.RegisterCommandsGloballyAsync();
    }
    private async Task HandleInteraction(SocketInteraction interaction)
    {
        try
        {
            var context = new SocketInteractionContext(_client, interaction);
            var result = await _handler.ExecuteCommandAsync(context, _services);
            if (!result.IsSuccess)
                switch (result.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        //_logger.LogError($"Occured Error while using command with id: {interaction.Id} by user: {context.User.Username}");
                        //await interaction.RespondAsync(result.ErrorReason, ephemeral: true);
                        break;
                    default:
                        break;
                }
        }
        catch
        {
            if (interaction.Type is InteractionType.ApplicationCommand)
                await interaction.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
        }
    }
    private async Task HandleInteractionExecute(ICommandInfo command, IInteractionContext context, IResult result)
    {
        if (!result.IsSuccess)
        {
            //_logger.LogError("Occured Error while using command: /{commandName} by user: {User}", command.Name, context.User.Username);
            switch (result.Error)
            {
                case InteractionCommandError.UnmetPrecondition:
                    await context.Interaction.RespondAsync($"Unmet Precondition: {result.ErrorReason}", ephemeral: true);
                    break;
                case InteractionCommandError.UnknownCommand:
                    await context.Interaction.RespondAsync("Unknown command", ephemeral: true);
                    break;
                case InteractionCommandError.BadArgs:
                    await context.Interaction.RespondAsync("Invalid number or arguments", ephemeral: true);
                    break;
                case InteractionCommandError.Exception:
                    await context.Interaction.RespondAsync($"Command exception: {result.ErrorReason}", ephemeral: true);
                    break;
                case InteractionCommandError.Unsuccessful:
                    await context.Interaction.RespondAsync("Command could not be executed", ephemeral: true);
                    break;
                default:
                    break;
            }
        }
    }
}