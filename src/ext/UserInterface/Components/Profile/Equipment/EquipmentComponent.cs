using Discord;
using RPGBot.Data;
using RPGBot.UserInterface.Buttons;
using RPGBot.UserInterface.SelectMenues;

namespace RPGBot.UserInterface;

public class EquipmentComponent : ComponentBuilder
{
    public EquipmentComponent(Dictionary<int, Item> equipment)
        => WithSelectMenu(new EquipmentSelectMenu(equipment));
}
