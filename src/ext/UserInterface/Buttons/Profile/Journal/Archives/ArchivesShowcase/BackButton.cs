using Discord;

namespace RPGBot.UserInterface.Buttons;

public class BackToArchivesButton : ButtonBuilder
{
    public BackToArchivesButton()
    {
        Label = "Back";
        Style = ButtonStyle.Secondary;
        CustomId = "backToArchivesButton";
    }
}
