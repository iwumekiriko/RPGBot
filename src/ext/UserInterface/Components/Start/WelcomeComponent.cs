using Discord;
using RPGBot.UserInterface.Buttons;
using RPGBot.UserInterface.SelectMenus;

namespace RPGBot.UserInterface;

public class WelcomeComponent : ComponentBuilder
{
    public WelcomeComponent()
        => WithButton(new NextButton())
          .WithButton(new CreditsButton());
}