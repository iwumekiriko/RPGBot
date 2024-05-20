using Discord;
using RPGBot.UserInterface.Buttons;
using RPGBot.UserInterface.SelectMenus;

namespace RPGBot.UserInterface;

public class ClassChoiceComponent : ComponentBuilder
{
    public ClassChoiceComponent(int classId = 0)
    {
        WithSelectMenu(new ClassSelectMenu());
        if (classId != 0) WithButton(new SubmitClassButton());
    }
}
