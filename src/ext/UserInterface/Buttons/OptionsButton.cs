using Discord;

namespace RPGBot.UserInterface.Buttons;

public class OptionsButton : ButtonBuilder
{
    public OptionsButton()
    {
        Label = "Options";
        Style = ButtonStyle.Secondary;
        CustomId = "optionsButton";
    }
}