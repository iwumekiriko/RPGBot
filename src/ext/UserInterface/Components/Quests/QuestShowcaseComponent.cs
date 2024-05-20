using Discord;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface;

public class QuestShowcaseComponent : ComponentBuilder
{
    public QuestShowcaseComponent(bool isStarted)
        => WithButton(isStarted ? new CompleteQuestButton() : new TakeQuestButton())
          .WithButton(new QuestBoardBackButton());
}