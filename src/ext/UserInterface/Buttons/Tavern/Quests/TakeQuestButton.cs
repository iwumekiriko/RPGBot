using Discord;

namespace RPGBot.UserInterface.Buttons;

public class TakeQuestButton : ButtonBuilder
{
    public TakeQuestButton()
    {
        Label = "Take Quest";
        Style = ButtonStyle.Success;
        CustomId = "takeQuestButton";
    }
}