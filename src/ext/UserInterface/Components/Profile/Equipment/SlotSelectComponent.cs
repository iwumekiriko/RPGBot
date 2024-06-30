using Discord;
using RPGBot.Data;
using RPGBot.ext.UserInterface.Buttons.Profile.Equipment;
using RPGBot.ext.UserInterface.Buttons.Profile.Journal;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface;

public class SlotSelectComponent : ComponentBuilder
{
    public SlotSelectComponent(Item item)
        => WithButton(item is Weapon ? new Slot1Button() : item is Accessory ? new Slot3Button() : null)
          .WithButton(item is Weapon ? new Slot2Button() : item is Accessory ? new Slot4Button() : null);
}
