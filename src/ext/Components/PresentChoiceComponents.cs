using Discord;
using RPGBot.Components.SelectMenues;

namespace RPGBot.Components;

public class PresentChoiceComponents : ComponentBuilder
{
    public PresentChoiceComponents()
        => WithSelectMenu(new PresentSelectMenu());
}
