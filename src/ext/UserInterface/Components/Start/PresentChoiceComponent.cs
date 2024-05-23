using Discord;
using RPGBot.Database.Models;
using RPGBot.UserInterface.Buttons;
using RPGBot.UserInterface.SelectMenus;

namespace RPGBot.UserInterface;

public class PresentChoiceComponent : ComponentBuilder
{
    public PresentChoiceComponent(int presentId = 0)
    {
        WithSelectMenu(new PresentSelectMenu([201, 202, 203]));
        if (presentId != 0) WithButton(new SubmitPresentButton());
    }
}