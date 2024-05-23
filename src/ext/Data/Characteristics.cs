namespace RPGBot.Data;

public class Health
{
    public int Value { get; set; }
    public int MaxValue { get; set; }
}
public class Armor
{
    public int Value { get; set; }
    public int MaxValue { get; set; }
}
public class Strength
{
    public int Value { get; set; }
    public int MaxValue { get; set; }
}
public class Dexterity
{
    public int Value { get; set; }
    public int MaxValue { get; set; }
}
public class Intellect
{
    public int Value { get; set; }
    public int MaxValue { get; set; }
}
public class Memory
{
    public int Value { get; set; }
    public int MaxValue { get; set; }
}
public class Conviction
{
    public int Value { get; set; }
    public int MaxValue { get; set; }
}
public class Money
{
    public int Value { get; set; } = 0;
    public int MaxValue { get; set; }
}
public class Experience
{
    public int Value { get; set; } = 0;
    public int MaxValue { get; set; }
}
public class Level
{
    public int Value { get; set; } = 1;
    public int MaxValue { get; set; }
}