using Discord.Interactions;

namespace RPGBot.UserInterface.Modals;
public class EquipModal : IModal
{
    public string Title => "Slot Selection";
    [InputLabel("Choose an equipment slot")]
    [ModalTextInput("slot", placeholder: "Slot Number", maxLength: 1)]
    public string Slot { get; set; }
}

