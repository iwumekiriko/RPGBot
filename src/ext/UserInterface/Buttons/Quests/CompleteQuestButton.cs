using Discord;

namespace RPGBot.UserInterface.Buttons;

public class CompleteQuestButton : ButtonBuilder
{
    public CompleteQuestButton()
    {
        Label = "Complete Quest";
        Style = ButtonStyle.Success;
        CustomId = "completeQuestButton";
    }
}