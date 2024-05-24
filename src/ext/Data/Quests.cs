using RPGBot.Database.Models;

namespace RPGBot.Data;

public class Quests
{
    private static Dictionary<int, Quest> _quests;
    public static Dictionary<int, Quest> GetQuests()
    {
        _quests ??= new Dictionary<int, Quest>()
        {
            [301] = new Quest
            {
                Id = 301,
                Name = "CoolQuest1",
                ShortDescription = "Go",
                FullDescription = "Go do smth",
                NeededToComplete = 5,
                RequiredLevel = 1,
                ExpReward = 10
            },
            [302] = new Quest
            {
                Id = 302,
                Name = "CoolQuest2",
                ShortDescription = "Go",
                FullDescription = "Go do smth again",
                NeededToComplete = 2,
                RequiredLevel = 5,
                ExpReward = 20
            },
        };
        return _quests;
    }
}
public partial class Quest
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string ShortDescription { get; set; }
    public required string FullDescription { get; set; }
    public int RequiredLevel { get; set; }
    public int? ItemId { get; set; }
    public int ExpReward { get; set; }
    public int MoneyReward { get; set; }
    public int NeededToComplete { get; set; }
}