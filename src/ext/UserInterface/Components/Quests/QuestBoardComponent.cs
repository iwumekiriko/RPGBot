﻿using Discord;
using RPGBot.UserInterface.SelectMenues;
using RPGBot.Data;

namespace RPGBot.UserInterface;

public class QuestBoardComponent : ComponentBuilder
{
    public QuestBoardComponent(Dictionary<Quest, bool> quests)
        => WithSelectMenu(new QuestBoardSelectMenu(quests));
}
