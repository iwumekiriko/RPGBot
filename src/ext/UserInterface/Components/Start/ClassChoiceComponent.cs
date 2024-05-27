using Discord;
using RPGBot.UserInterface.Buttons;
using RPGBot.UserInterface.SelectMenus;

namespace RPGBot.UserInterface;

public class ClassChoiceComponent : ComponentBuilder
{
    public ClassChoiceComponent(bool isSelected = false)
    {
        WithSelectMenu(new ClassSelectMenu());
        if (isSelected) WithButton(new SubmitClassButton());
    }
}
