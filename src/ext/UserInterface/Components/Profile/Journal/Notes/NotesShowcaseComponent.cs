using Discord;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface;

public class NotesShowcaseComponent : ComponentBuilder
{
    public NotesShowcaseComponent(bool isStarted)
        => WithButton(new NotesBackButton());
}