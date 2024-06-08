using Discord;

namespace RPGBot.UserInterface.Buttons;

public class PageAheadButton : ButtonBuilder
{
    public PageAheadButton()
    {
        Style = ButtonStyle.Primary;
        Emote = new Emoji("\U000025B6");
        CustomId = "pageAheadButton";
    }
}
