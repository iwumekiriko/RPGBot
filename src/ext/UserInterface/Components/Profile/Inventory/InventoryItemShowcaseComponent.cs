using Discord;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface;

public class InventoryItemShowcaseComponent : ComponentBuilder
{
    public InventoryItemShowcaseComponent()
        => WithButton(new UseItemButton())
          .WithButton(new DropItemButton())
          .WithButton(new DropItemsButton())
          .WithButton(new BackToInventoryButton());
}