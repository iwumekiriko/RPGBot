using Discord;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface;

public class JournalComponent : ComponentBuilder
{
    public JournalComponent()
        => WithButton(new NotesButton())
          .WithButton(new ArchivesButton())
          .WithButton(new MailButton())
          .WithButton(new MapButton());
}
