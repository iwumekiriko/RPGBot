using Discord;

namespace RPGBot.UserInterface.Buttons;

public class BackToMainButton : ButtonBuilder
{
    public BackToMainButton()
    {
        Label = "Back";
        Style = ButtonStyle.Secondary;
        CustomId = "backToMainButton";
    }
}
