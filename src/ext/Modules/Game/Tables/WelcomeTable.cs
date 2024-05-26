using Discord;
using Discord.Interactions;

using RPGBot.Database.Models;
using RPGBot.UserInterface;
using RPGBot.UserInterface.Embeds;
using RPGBot.Modules.Game.Services;
using RPGBot.Data;
using RPGBot.Utils.Embeds;
using RPGBot.Utils.Paths;
using System;

namespace RPGBot.Modules.Game;

public class WelcomeModule(IServiceProvider services) : BaseModule(services)
{
    private static GameClass playerClass;
    private static int presentId;

    [ComponentInteraction("nextButton")]
    public async Task NextHandler()
    {
        await SetPhase(1);

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
        var id = Convert.ToInt32(selections.First());
        var gameClass = Classes.GetClasses()[id];
        var url = await _images.GetImageUrlAsync($"{gameClass.Name}.png");
        playerClass = gameClass;

        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new OnlyImageEmbed(url).Build();
            message.Components = new ClassChoiceComponent(id).Build();
        });
    }
    [ComponentInteraction("submitClassButton")]
    public async Task SubmitClassButton()
    {
        await SetClass();
        var url = await _images.GetImageUrlAsync("Present.png");
        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new OnlyImageEmbed(url).Build();
            message.Components = new PresentChoiceComponent().Build();
        });
    }
    [ComponentInteraction("presentSelectMenu")]
    public async Task PresentChoiceHandler(string[] selections)
    {
        var id = Int32.Parse(selections.First());
        presentId = id;
        var item = Items.GetItems()[id];
        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new ItemShowcaseEmbed(item).Build();
            message.Components = new PresentChoiceComponent(presentId).Build();
        });
    }
    [ComponentInteraction("submitPresentButton")]
    public async Task SubmitPresentButton()
    {
        await SetPresent(presentId);

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = mainEmbed.Build();
            message.Components = mainComponents.Build();
        });
    }
    private async Task SetClass()
    {
        var player = await GetOrCreatePlayerAsync();
        await SetPhase(2, player);
        CopyStats(player, playerClass);
        await _database.SaveChangesAsync();
    }
    private async Task SetPresent(int presentId)
    {
        var player = await GetOrCreatePlayerAsync();
        await SetPhase(3, player);

        _database.Inventory.Where(
            i => i.UserId == player.UserId &&
                 i.GuildId == player.GuildId &&
                 i.ItemId == presentId
            ).First().Amount += 1;

        await _database.SaveChangesAsync();
    }
    private static void CopyStats(Player player, GameClass gameClass)
    {
        player.ClassId = gameClass.Id;
        player.Health = gameClass.Health.Value;
        player.Armor = gameClass.Armor.Value;
        player.Strength = gameClass.Strength.Value;
        player.Dexterity = gameClass.Dexterity.Value;
        player.Intellect = gameClass.Intellect.Value;
        player.Memory = gameClass.Memory.Value;
        player.Conviction = gameClass.Conviction.Value;
    }
    private async Task SetPhase(int phase, Player? player = null)
    {
        player ??= await GetOrCreatePlayerAsync();
        player.StartPhase = phase;
        await _database.SaveChangesAsync();
    }   
}
