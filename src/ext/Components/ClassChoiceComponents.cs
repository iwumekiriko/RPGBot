using Discord;
using RPGBot.Components.SelectMenues;

namespace RPGBot.Components;

public class ClassChoiceComponents : ComponentBuilder
{
    public ClassChoiceComponents()
        => WithSelectMenu(new ClassSelectMenu());
}
