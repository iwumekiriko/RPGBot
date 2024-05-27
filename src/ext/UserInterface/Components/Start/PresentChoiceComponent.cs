using Discord;
using RPGBot.Database.Models;
using RPGBot.UserInterface.Buttons;
using RPGBot.UserInterface.SelectMenus;

namespace RPGBot.UserInterface;

public class PresentChoiceComponent : ComponentBuilder
{
    public PresentChoiceComponent(bool isSelected = false)
    {
        WithSelectMenu(new PresentSelectMenu());
        if (isSelected) WithButton(new SubmitPresentButton());
    }
}