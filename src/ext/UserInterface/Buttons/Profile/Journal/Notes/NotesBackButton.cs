using Discord;

namespace RPGBot.UserInterface.Buttons;

public class NotesBackButton : ButtonBuilder
{
    public NotesBackButton()
    {
        Label = "Back";
        Style = ButtonStyle.Secondary;
        CustomId = "notesBackButton";
    }
}
