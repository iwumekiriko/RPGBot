using Discord;

namespace RPGBot.UserInterface.Buttons;

public class InventoryBackButton : ButtonBuilder
{
    public InventoryBackButton()
    {
        Label = "Back";
        Style = ButtonStyle.Primary;
        CustomId = "inventoryBackButton";
    }
}