using Discord;

namespace RPGBot.UserInterface.Buttons;

public class SubmitPresentButton : ButtonBuilder
{
    public SubmitPresentButton()
    {
        Label = "Submit";
        Style = ButtonStyle.Success;
        CustomId = "submitPresentButton";
    }
}
