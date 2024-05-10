using Discord;

namespace RPGBot.Components.Buttons;

public class QuestBoardButton : ButtonBuilder
{
    public QuestBoardButton()
    {
        Label = "Quest Board";
        Style = ButtonStyle.Primary;
        CustomId = "questBoardButton";
    }
}