using Discord;
using RPGBot.Data;
using RPGBot.UserInterface.Buttons;
using RPGBot.UserInterface.SelectMenues;

namespace RPGBot.UserInterface;

public class NotesComponent : ComponentBuilder
{
    public NotesComponent(List<Quest> quests)
        => WithSelectMenu(new ActiveQuestSelectMenu(quests))
          .WithButton(new BackToJournalButton());
}
