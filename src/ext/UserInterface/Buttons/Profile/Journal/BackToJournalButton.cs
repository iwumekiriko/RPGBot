using Discord;

namespace RPGBot.ext.UserInterface.Buttons.Profile.Journal.Notes;

public class BackToJournalButton : ButtonBuilder
{
    public BackToJournalButton()
    {
        Label = "Back";
        Style = ButtonStyle.Secondary;
        CustomId = "backToJournalButton";
    }
}
