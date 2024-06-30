using Discord;

namespace RPGBot.ext.UserInterface.Buttons.Profile.Equipment;

public class Slot4Button : ButtonBuilder
{
    public Slot4Button()
    {
        Label = "Slot 4";
        Style = ButtonStyle.Primary;
        CustomId = "slot4Button";
    }
}