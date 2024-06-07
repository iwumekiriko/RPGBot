using Discord;

namespace RPGBot.UserInterface.Buttons;

public class PageBackButton : ButtonBuilder
{
    public PageBackButton()
    {
        Style = ButtonStyle.Primary;
        Emote = new Emoji("\U000025C0");
        CustomId = "pageBackButton";
    }
}
