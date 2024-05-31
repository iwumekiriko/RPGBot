using Discord;

namespace RPGBot.UserInterface.Buttons;

public class NotesButton : ButtonBuilder
{
    public NotesButton()
    {
        Label = "Notes";
        Style = ButtonStyle.Primary;
        CustomId = "notesButton";
    }
}