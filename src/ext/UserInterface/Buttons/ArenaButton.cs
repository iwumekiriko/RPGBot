    using Discord;

namespace RPGBot.UserInterface.Buttons;

public class ArenaButton : ButtonBuilder
{   
    public ArenaButton()
    {
        Label = "Arena";
        Style = ButtonStyle.Danger;
        CustomId = "arenaButton";
    }
}
