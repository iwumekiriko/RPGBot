using Discord;
using RPGBot.Components.Buttons;
using RPGBot.Components.SelectMenues;
using RPGBot.Database;

namespace RPGBot.Components;

public class QuestBoardComponents : ComponentBuilder
{
    public QuestBoardComponents(Dictionary<Quest, bool> quests)
        => WithSelectMenu(new QuestBoardSelectMenu(quests));
}
public class QuestShowcaseComponents : ComponentBuilder
{
    public QuestShowcaseComponents(bool isStarted)
        => WithButton(isStarted ? new CompleteQuestButton() : new TakeQuestButton())
          .WithButton(new QuestBoardBackButton());
}