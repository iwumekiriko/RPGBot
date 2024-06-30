using Discord;

namespace RPGBot.ext.UserInterface.Buttons.Profile.Equipment;

public class Slot2Button : ButtonBuilder
{
    public Slot2Button()
    {
        Label = "Slot 2";
        Style = ButtonStyle.Primary;
        CustomId = "slot2Button";
    }
}