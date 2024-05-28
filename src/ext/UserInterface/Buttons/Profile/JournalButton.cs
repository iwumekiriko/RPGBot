using Discord;

namespace RPGBot.UserInterface.Buttons;

public class JournalButton : ButtonBuilder
{
    public JournalButton()
    {
        Label = "Journal";
        Style = ButtonStyle.Primary;
        CustomId = "journalButton";
    }
}