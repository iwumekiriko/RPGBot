using Discord;
using RPGBot.Data;
using RPGBot.ext.UserInterface.Buttons.Profile.Journal;
using RPGBot.UserInterface.Buttons;
using RPGBot.UserInterface.SelectMenues;

namespace RPGBot.UserInterface;

public class EquipmentShowcaseComponent : ComponentBuilder
{
    public EquipmentShowcaseComponent()
        => WithButton(new RemoveButton())
          .WithButton(new BackToEquipmentButton());
}
