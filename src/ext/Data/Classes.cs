using System.Net.Http.Headers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RPGBot.Data;

public class Classes
{
    private static Dictionary<int, GameClass> _classes;
    public static Dictionary<int, GameClass> GetClasses()
    {
        _classes ??= new Dictionary<int, GameClass>()
        {
            [101] = new GameClass
            {
                Id = 101,
                Name = "Warrior",
                Health = new Health { Value = 20 },
                Armor = new Armor { Value = 5 },
                Strength = new Strength { Value = 13 },
                Dexterity = new Dexterity { Value = 9 }, 
                Intellect = new Intellect { Value = 8 },
                Memory = new Memory { Value = 3 },
                Conviction = new Conviction { Value = 1 },
                PhotoLink = "https://imgur.com/kgeTFhq.png"
            },
            [102] = new GameClass
            { 
                Id = 102,
                Name = "Hunter",
                Health = new Health { Value = 20 },
                Armor = new Armor { Value = 5 },
                Strength = new Strength { Value = 8 },
                Dexterity = new Dexterity { Value = 13 },
                Intellect = new Intellect { Value = 9 },
                Memory = new Memory { Value = 3 },
                Conviction = new Conviction { Value = 1 },
                PhotoLink = "https://imgur.com/qyYZeML.png"
            },
            [103] = new GameClass
            { 
                Id = 101,
                Name = "Mage",
                Health = new Health { Value = 20 },
                Armor = new Armor { Value = 5 },
                Strength = new Strength { Value = 9 },
                Dexterity = new Dexterity { Value = 8 },
                Intellect = new Intellect { Value = 13 },
                Memory = new Memory { Value = 3 },
                Conviction = new Conviction { Value = 1 },
                PhotoLink = "https://imgur.com/kgeTFhq.png"
            },
        };

        return _classes;
    }
}
public class GameClass
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Health Health { get; set; }
    public Armor Armor { get; set; }
    public Strength Strength { get; set; }
    public Dexterity Dexterity { get; set; }
    public Intellect Intellect { get; set; }
    public Memory Memory { get; set; }
    public Conviction Conviction { get; set; }
    public string PhotoLink { get; set; }
}