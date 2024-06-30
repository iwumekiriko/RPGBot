using Discord;
using RPGBot.Data;

namespace RPGBot.UserInterface.SelectMenues;

public class LocationSelectMenu : SelectMenuBuilder
{
    public LocationSelectMenu()
    {
        Placeholder = "Select Location";
        CustomId = "locationSelectMenu";
        MinValues = 1;
        MaxValues = 1;
        AddOption(
            label: "Town",
            value: "town"
        );
        AddOption(
            label: "Forest",
            value: "forest"
        );
        //foreach (var item in items)
        //    AddOption(
        //        label: item.Key.Name,
        //        value: item.Key.Id.ToString()
        //    );
    }
}

