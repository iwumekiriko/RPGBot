using Discord;

namespace RPGBot.ext.UserInterface.Buttons.Profile.Journal;

public class RemoveButton : ButtonBuilder
{
    public RemoveButton()
    {
        Label = "Remove";
        Style = ButtonStyle.Danger;
        CustomId = "removeButton";
    }
}
