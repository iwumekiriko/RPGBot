using Discord;

namespace RPGBot.UserInterface.Buttons;

public class SearchButton : ButtonBuilder
{
    public SearchButton()
    {
        Label = "1/1";
        Style = ButtonStyle.Secondary;
        CustomId = "searchButton";
    }
}