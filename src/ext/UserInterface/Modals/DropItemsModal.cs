using Discord.Interactions;

namespace RPGBot.UserInterface.Modals;

public class DropItemsModal : IModal
{
    public string Title => "How many items?";
    [InputLabel("Amount")]
    [ModalTextInput("itemsAmount", placeholder: "1", maxLength: 3)]
    public string Amount {get; set;}
}
