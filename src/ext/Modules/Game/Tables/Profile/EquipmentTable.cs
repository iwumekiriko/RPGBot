using Discord.Interactions;

using RPGBot.UserInterface;
using RPGBot.UserInterface.Embeds;
using RPGBot.Modules.Game.Services;
using RPGBot.Data;
using Discord;

namespace RPGBot.Modules.Game;

public class EquipmentTable(IServiceProvider services) : BaseModule(services)
{
    private static int CurrentItemId { get; set; }

    [ComponentInteraction("equipmentSelectMenu")]
    public async Task InventoryItemShowcase(string[] selections)
    {
        var itemId = int.Parse(selections.First());
        var item = _inventory.GetItem(itemId) ??
            throw new InvalidDataException();
        CurrentItemId = itemId;
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new InventoryItemShowcaseEmbed(item).Build();
            message.Components = new EquipmentShowcaseComponent().Build();
        });
    }
    [ComponentInteraction("backToEquipmentButton")]
    public async Task BackToEquipmentButton()
    {
        var player = await GetOrCreatePlayerAsync();
        var playerEquipment = await _equipment.GetPlayerEquipment(player);
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new EquipmentEmbed(playerEquipment).Build();
            message.Components = new EquipmentComponent(playerEquipment).Build();
        });
    }
    [ComponentInteraction("removeButton")]
    public async Task RemoveButton()
    {
        var player = await GetOrCreatePlayerAsync();
        await _equipment.RemoveItem(player, CurrentItemId);
        var playerEquipment = await _equipment.GetPlayerEquipment(player);
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new EquipmentEmbed(playerEquipment).Build();
            message.Components = new EquipmentComponent(playerEquipment).Build();
        });
    }
}
