using Discord;
using RPGBot.Data;
using RPGBot.ext.UserInterface.Buttons.Profile.Journal.Notes;
using RPGBot.UserInterface.Buttons;
using RPGBot.UserInterface.SelectMenues;

namespace RPGBot.UserInterface;

public class MapComponent : ComponentBuilder
{
    public MapComponent()
        => WithSelectMenu(new LocationSelectMenu())
          .WithButton(new BackToJournalButton());
}
