using Discord;
using RPGBot.Components.Buttons;
using RPGBot.Components.SelectMenus;

namespace RPGBot.Components;

public class WelcomeComponents : ComponentBuilder
{
    public WelcomeComponents()
        => WithButton(new NextButton())
          .WithButton(new CreditsButton());
}
public class ClassChoiceComponents : ComponentBuilder
{
    public ClassChoiceComponents(int classId = 0)
    {
        WithSelectMenu(new ClassSelectMenu());
        if (classId != 0) WithButton(new SubmitClassButton());
    }
}
public class PresentChoiceComponents : ComponentBuilder
{
    public PresentChoiceComponents()
        => WithSelectMenu(new PresentSelectMenu());
}