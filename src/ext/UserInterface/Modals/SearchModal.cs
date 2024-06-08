using Discord;
using RPGBot.Data;
using System.Reflection.Emit;

namespace RPGBot.UserInterface.SelectMenus;

public class SearchModal : ModalBuilder
{
    public SearchModal()
    {
        Title = "Search";
        CustomId = "searchModal";
        AddTextInput("What do you want to find?", "search", placeholder: "Name or ID");
    }
}

