using Discord;
using RPGBot.Data;
using RPGBot.ext.UserInterface.Buttons.Profile.Journal.Notes;
using RPGBot.UserInterface.Buttons;
using RPGBot.UserInterface.SelectMenues;

namespace RPGBot.UserInterface;

public class ArchivesComponent : ComponentBuilder
{
    public ArchivesComponent()
        => WithSelectMenu(new ArchivesSelectMenu())
          .WithButton(new BackToJournalButton());
}
