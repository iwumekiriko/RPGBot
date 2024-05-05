using Discord;

namespace RPGBot.Components.Buttons;

public class ArenaButton : ButtonBuilder
{
    public ArenaButton()
    {
        Label = "Arena";
        Style = ButtonStyle.Danger;
        CustomId = "arenaButton";
    }
}
