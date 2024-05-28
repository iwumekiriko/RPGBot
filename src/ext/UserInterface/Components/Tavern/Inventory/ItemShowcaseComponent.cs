using Discord;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface;

public class ItemShowcaseComponent : ComponentBuilder
{
    public ItemShowcaseComponent()
        => WithButton(new UseItemButton())
          .WithButton(new DropItemButton())
          .WithButton(new InventoryBackButton());
}