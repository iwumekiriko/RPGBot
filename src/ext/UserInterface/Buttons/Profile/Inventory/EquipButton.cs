using Discord;

namespace RPGBot.ext.UserInterface.Buttons.Profile.Journal;

public class EquipButton : ButtonBuilder
{
    public EquipButton()
    {
        Label = "Equip";
        Style = ButtonStyle.Success;
        CustomId = "equipButton";
    }
}