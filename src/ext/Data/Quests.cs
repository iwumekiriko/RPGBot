using RPGBot.Database;

namespace RPGBot.Data;

public class Quests
{
    public static Quest[] GetQuests()
    {
        var quest1 = new Quest() { Id = 1, Name = "CoolQuest1", ShortDescription = "Go", FullDescription = "Go do smth", NeededToComplete = 5, RequiredLevel = 1, ExpReward = 10 };
        var quest2 = new Quest() { Id = 2, Name = "CoolQuest2", ShortDescription = "Go", FullDescription = "Go do smth again", NeededToComplete = 2, RequiredLevel = 5, ExpReward = 20 };

        return [quest1, quest2];
    }
}