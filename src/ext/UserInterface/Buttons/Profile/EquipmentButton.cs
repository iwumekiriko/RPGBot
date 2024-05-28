using Discord;

namespace RPGBot.UserInterface.Buttons;

public class EquipmentButton : ButtonBuilder
{
    public EquipmentButton()
    {
        Label = "Equipment";
        Style = ButtonStyle.Primary;
        CustomId = "equipmentButton";
    }
}