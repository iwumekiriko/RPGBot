using Discord.Interactions;
using Microsoft.Extensions.Logging;
using RPGBot.Database;
using RPGBot.UserInterface;
using RPGBot.UserInterface.Embeds;
using Discord;
using Microsoft.EntityFrameworkCore;
using RPGBot.Modules.Game.Services;
using RPGBot.Data;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;

namespace RPGBot.Modules.Game;

public class WelcomeModule(IServiceProvider services) : BaseModule(services)
{
    private static readonly EmbedBuilder _classesEmbed = new ClassChoiceEmbed();
    private static readonly EmbedBuilder _presentsEmbed = new PresentChoiceEmbed();

    private static int classId;

    [ComponentInteraction("nextButton")]
    public async Task NextHandler()
    {
        await SetPhase(1);

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = _classesEmbed.Build();
            message.Components = new ClassChoiceComponent(classId).Build();
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
        classId = id;

        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new ClassShowcaseEmbed(classId).Build();
            message.Components = new ClassChoiceComponent(classId).Build();
        });
    }
    [ComponentInteraction("submitClassButton")]
    public async Task SubmitClassButton()
    {
        await SetClass(classId);

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = _presentsEmbed.Build();
            message.Components = new PresentChoiceComponent().Build();
        });
    }

    [ComponentInteraction("presentSelectMenu")]
    public async Task PresentChoiceHandler(string[] selections)
    {
        await SetPresent(selections);

        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = mainEmbed.Build();
            message.Components = mainComponents.Build();
        });
    }
    private async Task SetClass(int classId)
    {
        var player = await GetOrCreatePlayerAsync();
        var playerClass = Classes.GetClasses()[classId];
        await SetPhase(2, player);
        player.ClassId = classId;

        var classRef = Activator.CreateInstance(playerClass) as GameClass;
        foreach (var propertyInfo in typeof(GameClass).GetProperties())
        {
            var value = propertyInfo.GetValue(classRef);
            player.GetType()
                .GetProperty(propertyInfo.Name)
                .SetValue(player, value);
        }
        await _database.SaveChangesAsync();
    }
    private async Task SetPresent(string[] selections)
    {
        var player = await GetOrCreatePlayerAsync();
        await SetPhase(3, player);

        _database.Inventory.Where(
            i => i.UserId == player.UserId &&
                 i.GuildId == player.GuildId &&
                 i.ItemId == int.Parse(selections.First())
            ).First().Amount += 1;

        await _database.SaveChangesAsync();
    }
    private async Task SetPhase(int phase, Player? player = null)
    {
        player ??= await GetOrCreatePlayerAsync();
        player.StartPhase = phase;
        await _database.SaveChangesAsync();
    }
    
}
