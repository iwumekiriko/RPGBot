using Discord;

namespace RPGBot.Components.Buttons;

public class JourneyButton : ButtonBuilder
{
    public JourneyButton()
    {
        Label = "Journey";
        Style = ButtonStyle.Success;
        CustomId = "journeyButton";
    }
}
