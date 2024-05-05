using Discord;

namespace RPGBot.Components.Buttons;

public class InventoryButton : ButtonBuilder
{
    public InventoryButton()
    {
        Label = "Inventory";
        Style = ButtonStyle.Primary;
        CustomId = "inventoryButton";
    }
}
