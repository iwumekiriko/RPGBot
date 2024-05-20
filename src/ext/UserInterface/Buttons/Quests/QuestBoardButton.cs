using Discord;

namespace RPGBot.UserInterface.Buttons;

public class QuestBoardButton : ButtonBuilder
{
    public QuestBoardButton()
    {
        Label = "Quest Board";
        Style = ButtonStyle.Primary;
        CustomId = "questBoardButton";
    }
}