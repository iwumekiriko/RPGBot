using Discord;
using RPGBot.UserInterface.Buttons;
using RPGBot.UserInterface.SelectMenues;
using RPGBot.Database.Models;

namespace RPGBot.UserInterface;

public class QuestBoardComponent : ComponentBuilder
{
    public QuestBoardComponent(Dictionary<Quest, bool> quests)
        => WithSelectMenu(new QuestBoardSelectMenu(quests));
}
