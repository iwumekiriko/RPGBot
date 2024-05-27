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
                BaseHealth = 20,
                Armor = 5,
                Strength = 13,
                Dexterity = 9, 
                Intellect = 8,
                Memory = 3,
                Conviction = 1,
            },
            [102] = new GameClass
            { 
                Id = 102,
                Name = "Hunter",
                BaseHealth = 20,
                Armor = 5,
                Strength = 8,
                Dexterity = 13,
                Intellect = 9,
                Memory = 3,
                Conviction = 1,
            },
            [103] = new GameClass
            { 
                Id = 101,
                Name = "Mage",
                BaseHealth = 20,
                Armor = 5,
                Strength = 9,
                Dexterity = 8,
                Intellect = 13,
                Memory = 3,
                Conviction = 1,
            },
        };

        return _classes;
    }
}
public class GameClass
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BaseHealth { get; set; }
    public int Armor { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Intellect { get; set; }
    public int Memory { get; set; }
    public int Conviction { get; set; }
}