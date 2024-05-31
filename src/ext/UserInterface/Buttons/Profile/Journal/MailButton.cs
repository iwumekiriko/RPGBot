using Discord;

namespace RPGBot.UserInterface.Buttons;

public class MailButton : ButtonBuilder
{
    public MailButton()
    {
        Label = "Mail";
        Style = ButtonStyle.Primary;
        CustomId = "mailButton";
    }
}