﻿    using Discord.Interactions;

using RPGBot.UserInterface.Embeds;
using RPGBot.UserInterface;
using RPGBot.Data;
using RPGBot.Modules.Game.Services;

namespace RPGBot.Modules.Game;

public class ProfileTable(IServiceProvider services) : BaseModule(services)
{
    [ComponentInteraction("equipmentButton")]
    public async Task EquipmentHandler()
    {
        var player = await GetOrCreatePlayerAsync();
        var playerEquipment = await _equipment.GetPlayerEquipment(player);
        await DeferAsync();
        await FollowupAsync(
            embed: new EquipmentEmbed(playerEquipment).Build(),
            components: new EquipmentComponent(playerEquipment).Build(),
            ephemeral: true
        );
    }
    [ComponentInteraction("inventoryButton")]
    public async Task InventoryHandler()
    {
        var player = await GetOrCreatePlayerAsync();
        var playerItems = await _inventory.GetPlayerItems(player);
        await DeferAsync();
        await FollowupAsync(
            embed: new InventoryEmbed(playerItems).Build(),
            components: new InventoryComponent(playerItems).Build(),
            ephemeral: true
        );
    }
    [ComponentInteraction("journalButton")]
    public async Task JournalHandler()
    {
        await DeferAsync();
        await FollowupAsync(
            embed: new JournalEmbed().Build(),
            components: new JournalComponent().Build(),
            ephemeral: true
        );
    }
}
