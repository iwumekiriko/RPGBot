using RPGBot.Database;

namespace RPGBot.Data;

public class Classes
{
    public static Dictionary<int, Type> GetClasses()
    {
        var classes = new Dictionary<int, Type>()
        {
            [101] = typeof(Warrior),
            [102] = typeof(Hunter),
            [103] = typeof(Mage),
        };
        return classes;
    }
}
