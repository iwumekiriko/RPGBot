using Discord;

namespace RPGBot.UserInterface.Buttons;

public class NextButton : ButtonBuilder
{
    public NextButton()
    {
        Label = "Next";
        Style = ButtonStyle.Success;
        CustomId = "nextButton";
    }
}