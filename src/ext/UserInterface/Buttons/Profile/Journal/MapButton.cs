using Discord;

namespace RPGBot.UserInterface.Buttons;

public class MapButton : ButtonBuilder
{
    public MapButton()
    {
        Label = "Map";
        Style = ButtonStyle.Secondary;
        CustomId = "mapButton";
    }
}