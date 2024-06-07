using Discord;

namespace RPGBot.UserInterface.Buttons;

public class BackToQuestBoardButton : ButtonBuilder
{
    public BackToQuestBoardButton()
    {
        Label = "Back";
        Style = ButtonStyle.Secondary;
        CustomId = "backToQuestBoardButton";
    }
}