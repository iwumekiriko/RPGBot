using Discord;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface;

public class ProfileComponent : ComponentBuilder
{
    public ProfileComponent()
        => WithButton(new EquipmentButton())
          .WithButton(new InventoryButton())
          .WithButton(new JournalButton())
          .WithButton(new BackToMainButton());
}
