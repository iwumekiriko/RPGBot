using Discord;

namespace RPGBot.UserInterface.Modals;

public class SearchModal : ModalBuilder
{
    public SearchModal()
    {
        Title = "Search";
        CustomId = "searchModal";
        AddTextInput("What do you want to find?", "search", placeholder: "Name or ID");
    }
}

