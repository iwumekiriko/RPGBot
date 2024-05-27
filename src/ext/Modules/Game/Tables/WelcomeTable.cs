using Discord;
using Discord.Interactions;

using RPGBot.Database.Models;
using RPGBot.UserInterface;
using RPGBot.UserInterface.Embeds;
using RPGBot.Data;
using RPGBot.Utils.Embeds;
using RPGBot.Utils.Paths;
using System;
using RPGBot.Modules.Game.Services;

namespace RPGBot.Modules.Game;

public class WelcomeModule(IServiceProvider services) : BaseModule(services)
{
    private static GameClass playerClass;
    private static Item playerPresent;

    [ComponentInteraction("nextButton")]
    public async Task NextHandler()
    {
        await SetStartPhase(1);
        var url = await _images.GetImageUrlAsync("Class.png");
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new OnlyImageEmbed(url).Build();
            message.Components = new ClassChoiceComponent().Build();
        });
    }
    [ComponentInteraction("creditsButton")]
    public async Task CreditsHandler()
    {
        await Context.Interaction.DeferAsync();
        await FollowupAsync(
            "test",
            ephemeral: true
        );
    }
    [ComponentInteraction("classSelectMenu")]
    public async Task ClassShowcase(string[] selections)
    {
        playerClass = Classes.GetClasses()[int.Parse(selections.First())];
        var url = await _images.GetImageUrlAsync($"{playerClass.Name}.png");
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new OnlyImageEmbed(url).Build();
            message.Components = new ClassChoiceComponent(true).Build();
        });
    }
    [ComponentInteraction("submitClassButton")]
    public async Task SubmitClassButton()
    {
        await SetClass();
        var url = await _images.GetImageUrlAsync("Present.png");
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new OnlyImageEmbed(url).Build();
            message.Components = new PresentChoiceComponent().Build();
        });
    }
    [ComponentInteraction("presentSelectMenu")]
    public async Task PresentChoiceHandler(string[] selections)
    {
        playerPresent = Items.GetItems()[int.Parse(selections.First())];
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new ItemShowcaseEmbed(playerPresent).Build();
            message.Components = new PresentChoiceComponent(true).Build();
        });
    }
    [ComponentInteraction("submitPresentButton")]
    public async Task SubmitPresentButton()
    {
        await SetPresent();
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = mainEmbed.Build();
            message.Components = mainComponents.Build();
        });
    }
    private async Task SetClass()
    {
        var player = await GetOrCreatePlayerAsync();
        await SetStartPhase(2, player);
        CopyStats(player, playerClass);
    }
    private async Task SetPresent()
    {
        var player = await GetOrCreatePlayerAsync();
        await SetStartPhase(3, player);
        _inventory.AddItemToInventory(
            player.GuildId, player.UserId, playerPresent.Id
        );
    }
    private async void CopyStats(Player player, GameClass gameClass)
    {
        player.ClassId = gameClass.Id;
        player.CurrentHealth = gameClass.BaseHealth;
        player.MaxHealth = gameClass.BaseHealth;
        player.Armor = gameClass.Armor;
        player.Strength = gameClass.Strength;
        player.Dexterity = gameClass.Dexterity;
        player.Intellect = gameClass.Intellect;
        player.Memory = gameClass.Memory;
        player.Conviction = gameClass.Conviction;

        await _database.SaveChangesAsync();
    }
    private async Task SetStartPhase(int phase, Player? player = null)
    {
        player ??= await GetOrCreatePlayerAsync();
        player.StartPhase = phase;
        await _database.SaveChangesAsync();
    }   
}
