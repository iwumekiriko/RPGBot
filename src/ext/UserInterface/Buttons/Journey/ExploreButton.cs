using Discord;

namespace RPGBot.UserInterface.Buttons;

public class ExploreButton : ButtonBuilder
{
    public ExploreButton()
    {
        Label = "Explore";
        Style = ButtonStyle.Success;
        CustomId = "exploreButton";
    }
}
