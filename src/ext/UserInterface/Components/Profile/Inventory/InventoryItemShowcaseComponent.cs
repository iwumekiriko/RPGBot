using Discord;
using RPGBot.Data;
using RPGBot.ext.UserInterface.Buttons.Profile.Journal;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface;

public class InventoryItemShowcaseComponent : ComponentBuilder
{
    public InventoryItemShowcaseComponent(Item item)
        => WithButton(new UseItemButton())
          .WithButton(item is Weapon || item is Accessory ? new EquipButton() : null)
          .WithButton(new DropItemButton())
          .WithButton(new DropItemsButton())
          .WithButton(new BackToInventoryButton());
}