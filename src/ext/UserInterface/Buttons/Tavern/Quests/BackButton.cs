using Discord;

namespace RPGBot.UserInterface.Buttons;

public class QuestBoardBackButton : ButtonBuilder
{
    public QuestBoardBackButton()
    {
        Label = "Back";
        Style = ButtonStyle.Primary;
        CustomId = "questBoardBackButton";
    }
}