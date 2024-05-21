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
public abstract class GameClass
{
    public abstract int Id { get; }
    public abstract int Health { get; }
    public abstract int Armor { get; }
    public abstract int Strength { get; }
    public abstract int Dexterity { get; }
    public abstract int Intellect { get; }
    public abstract int Memory { get; }
    public abstract int Conviction { get; }
}

public partial class Warrior : GameClass
{
    public override int Id { get => 101; }
    public override int Health { get => 20; }
    public override int Armor { get => 5; }
    public override int Strength { get => 13; }
    public override int Dexterity { get => 9; }
    public override int Intellect { get => 8; }
    public override int Memory { get => 3; }
    public override int Conviction { get => 1; }
}
public partial class Hunter : GameClass
{
    public override int Id { get => 102; }
    public override int Health { get => 20; }
    public override int Armor { get => 4; }
    public override int Strength { get => 8; }
    public override int Dexterity { get => 13; }
    public override int Intellect { get => 9; }
    public override int Memory { get => 3; }
    public override int Conviction { get => 1; }
}
public partial class Mage : GameClass
{
    public override int Id { get => 103; }
    public override int Health { get => 20; }
    public override int Armor { get => 3; }
    public override int Strength { get => 9; }
    public override int Dexterity { get => 8; }
    public override int Intellect { get => 13; }
    public override int Memory { get => 3; }
    public override int Conviction { get => 1; }
}