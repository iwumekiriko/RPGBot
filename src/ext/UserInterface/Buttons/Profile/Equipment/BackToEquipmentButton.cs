using Discord;

namespace RPGBot.ext.UserInterface.Buttons.Profile.Journal;

public class BackToEquipmentButton : ButtonBuilder
{
    public BackToEquipmentButton()
    {
        Label = "Back";
        Style = ButtonStyle.Secondary;
        CustomId = "backToEquipmentButton";
    }
}
