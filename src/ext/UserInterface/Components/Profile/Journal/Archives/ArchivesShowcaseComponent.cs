using Discord;
using RPGBot.Data;
using RPGBot.UserInterface.Buttons;
using RPGBot.UserInterface.SelectMenues;

namespace RPGBot.UserInterface;

public class ArchivesShowcaseComponent : ComponentBuilder
{
    public ArchivesShowcaseComponent()
        => WithButton(new PageBackButton())
          .WithButton(new PageAheadButton())
          .WithButton(new SearchButton())
          .WithButton(new BackToArchivesButton());
}
