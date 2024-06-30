using Discord;

namespace RPGBot.ext.UserInterface.Buttons.Profile.Equipment;

public class Slot1Button : ButtonBuilder
{
    public Slot1Button()
    {
        Label = "Slot 1";
        Style = ButtonStyle.Primary;
        CustomId = "slot1Button";
    }
}