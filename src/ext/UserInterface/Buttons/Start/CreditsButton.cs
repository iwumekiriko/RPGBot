using Discord;

namespace RPGBot.UserInterface.Buttons;

public class CreditsButton : ButtonBuilder
{
    public CreditsButton()
    {
        Label = "Credits";
        Style = ButtonStyle.Secondary;
        CustomId = "creditsButton";
    }
}
