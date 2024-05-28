using Discord;

namespace RPGBot.UserInterface.Buttons;

public class TavernButton : ButtonBuilder
{
    public TavernButton()
    {
        Label = "Tavern";
        Style = ButtonStyle.Primary;
        CustomId = "tavernButton";
    }
}