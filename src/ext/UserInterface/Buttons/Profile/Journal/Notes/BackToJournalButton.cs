using Discord;

namespace RPGBot.UserInterface.Buttons;

public class BackToJournalButton : ButtonBuilder
{
    public BackToJournalButton()
    {
        Label = "Back";
        Style = ButtonStyle.Secondary;
        CustomId = "backToJournalButton";
    }
}
