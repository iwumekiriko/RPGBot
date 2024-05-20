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
public class QuestBoardBackButton : ButtonBuilder
{
    public QuestBoardBackButton()
    {
        Label = "Back";
        Style = ButtonStyle.Primary;
        CustomId = "questBoardBackButton";
    }
}
public class TakeQuestButton : ButtonBuilder
{
    public TakeQuestButton()
    {
        Label = "Take Quest";
        Style = ButtonStyle.Success;
        CustomId = "takeQuestButton";
    }
}
public class CompleteQuestButton : ButtonBuilder
{
    public CompleteQuestButton()
    {
        Label = "Complete Quest";
        Style = ButtonStyle.Success;
        CustomId = "completeQuestButton";
    }
}