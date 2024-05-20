using Discord;
using RPGBot.UserInterface.Buttons;
using RPGBot.UserInterface.SelectMenus;

namespace RPGBot.UserInterface;

public class PresentChoiceComponent : ComponentBuilder
{
    public PresentChoiceComponent()
        => WithSelectMenu(new PresentSelectMenu());
}