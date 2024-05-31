using Discord;
using RPGBot.UserInterface.SelectMenus;
using RPGBot.Data;

namespace RPGBot.UserInterface;

public class QuestBoardComponent : ComponentBuilder
{
    public QuestBoardComponent(List<Quest> quests)
        => WithSelectMenu(new QuestBoardSelectMenu(quests));
}
