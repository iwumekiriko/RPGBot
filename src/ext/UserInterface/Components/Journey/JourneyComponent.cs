using Discord;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface;

public class JourneyComponent : ComponentBuilder
{
    public JourneyComponent()
        => WithButton(new ExploreButton())
          .WithButton(new ArenaButton())
          .WithButton(new BackToMainButton());
}
