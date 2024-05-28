using Discord;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface;

public class OptionsComponent : ComponentBuilder
{
    public OptionsComponent()
        => WithButton(new BackToMainButton());
}
