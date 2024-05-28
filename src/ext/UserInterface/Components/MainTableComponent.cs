using Discord;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface;

public class MainTableComponent : ComponentBuilder
{
    public MainTableComponent()
        => WithButton(new ProfileButton())
          .WithButton(new TavernButton())
          .WithButton(new JourneyButton())
          .WithButton(new OptionsButton());
}
