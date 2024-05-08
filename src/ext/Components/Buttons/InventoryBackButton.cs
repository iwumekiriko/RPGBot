using Discord;

namespace RPGBot.Components.Buttons;

public class InventoryBackButton : ButtonBuilder
{
    public InventoryBackButton()
    {
        Label = "Back";
        Style = ButtonStyle.Primary;
        CustomId = "inventoryBackButton";
    }
}