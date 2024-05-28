using Discord;

namespace RPGBot.UserInterface.Buttons;

public class InventoryButton : ButtonBuilder
{
    public InventoryButton()
    {
        Label = "Inventory";
        Style = ButtonStyle.Primary;
        CustomId = "inventoryButton";
    }
}