using Discord;
using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using RPGBot.UserInterface;
using RPGBot.UserInterface.Embeds;
using RPGBot.Database.Models;
using RPGBot.Modules.Game.Services;


namespace RPGBot.Modules.Game;

public class MainModule(IServiceProvider services) : BaseModule(services)
{
    [ComponentInteraction("profileButton")]
    public async Task ProfileHandler()
    {
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new ProfileEmbed().Build();
            message.Components = new ProfileComponent().Build();
        });
    }
    [ComponentInteraction("tavernButton")]
    public async Task TavernHandler()
    {
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new TavernEmbed().Build();
            message.Components = new TavernComponent().Build();
        });
    }
    [ComponentInteraction("journeyButton")]
    public async Task JourneyHandler()
    {
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new JourneyEmbed().Build();
            message.Components = new JourneyComponent().Build();
        });
    }
    [ComponentInteraction("optionsButton")]
    public async Task OptionsHandler()
    {
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new OptionsEmbed().Build();
            message.Components = new OptionsComponent().Build();
        });
    }
    [ComponentInteraction("backToMainButton")]
    public async Task BackToMain()
    {
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Content = null;
            message.Embed = mainEmbed.Build();
            message.Components = mainComponent.Build();
        });
    }
}
