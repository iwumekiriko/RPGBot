using Discord;
using RPGBot.Data;

namespace RPGBot.UserInterface.SelectMenues;

public class EquipmentSelectMenu : SelectMenuBuilder
{
    public EquipmentSelectMenu(Dictionary<int, Item> equipment)
    {
        Placeholder = "Select Equipment";
        CustomId = "equipmentSelectMenu";
        MinValues = 1;
        MaxValues = 1;
        if (equipment.Count == 0)
        {
            AddOption(
                label: "Empty",
                value: "None"
            );
            IsDisabled = true;
        }
        else
        {
            foreach (var item in equipment)
                AddOption(
                    label: item.Value.Name,
                    value: item.Value.Id.ToString()
                );
        }
        //AddOption(
        //    label: item.Key.Name,
        //    value: item.Key.Id.ToString()
        //);
        //AddOption(
        //    label: "Slot 2",
        //    value: "2"
        //);
        //AddOption(
        //    label: "Slot 3",
        //    value: "3"
        //);
        //AddOption(
        //    label: "Slot 4",
        //    value: "4"
        //);
    }
}

