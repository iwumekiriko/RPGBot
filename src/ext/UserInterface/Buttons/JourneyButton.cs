using Discord;

namespace RPGBot.UserInterface.Buttons;

public class JourneyButton : ButtonBuilder
{
    public JourneyButton()
    {
        Label = "Journey";
        Style = ButtonStyle.Success;
        CustomId = "journeyButton";
    }
}
