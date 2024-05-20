using Discord;
using RPGBot.Components.Buttons;
using RPGBot.Components.SelectMenues;

namespace RPGBot.Components;

public class WelcomeComponents : ComponentBuilder
{
    public WelcomeComponents()
        => WithButton(new NextButton())
          .WithButton(new CreditsButton());
}
public class ClassChoiceComponents : ComponentBuilder
{
    public ClassChoiceComponents()
        => WithSelectMenu(new ClassSelectMenu())
          .WithButton(new SubmitClassButton());
}
public class PresentChoiceComponents : ComponentBuilder
{
    public PresentChoiceComponents()
        => WithSelectMenu(new PresentSelectMenu());
}