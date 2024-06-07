using Discord;

namespace RPGBot.UserInterface.Buttons;

public class BackToNotesButton : ButtonBuilder
{
    public BackToNotesButton()
    {
        Label = "Back";
        Style = ButtonStyle.Secondary;
        CustomId = "backToNotesButton";
    }
}
