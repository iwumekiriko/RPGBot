using Discord;

namespace RPGBot.ext.UserInterface.Buttons.Profile.Equipment;

public class Slot3Button : ButtonBuilder
{
    public Slot3Button()
    {
        Label = "Slot 3";
        Style = ButtonStyle.Primary;
        CustomId = "slot3Button";
    }
}