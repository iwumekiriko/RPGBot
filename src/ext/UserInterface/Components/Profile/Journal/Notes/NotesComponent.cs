using Discord;
using RPGBot.Data;
using RPGBot.UserInterface.Buttons;
using RPGBot.UserInterface.SelectMenues;

namespace RPGBot.UserInterface;

public class NotesComponent : ComponentBuilder
{
    public NotesComponent(Dictionary<Quest, Tuple<int, bool>> quests)
        => WithSelectMenu(new ActiveQuestSelectMenu(quests))
          .WithButton(new BackToJournalButton());
}
