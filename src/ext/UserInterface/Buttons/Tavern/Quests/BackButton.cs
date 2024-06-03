using Discord;

namespace RPGBot.UserInterface.Buttons;

public class BackToQuestBoardButton : ButtonBuilder
{
    public BackToQuestBoardButton()
    {
        Label = "Back to quest board";
        Style = ButtonStyle.Secondary;
        CustomId = "backToQuestBoardButton";
    }
}