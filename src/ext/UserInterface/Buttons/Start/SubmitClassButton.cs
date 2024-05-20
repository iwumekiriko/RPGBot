using Discord;

namespace RPGBot.UserInterface.Buttons;

public class SubmitClassButton : ButtonBuilder
{
    public SubmitClassButton()
    {
        Label = "Submit";
        Style = ButtonStyle.Success;
        CustomId = "submitClassButton";
    }
}
