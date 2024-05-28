using Discord;

namespace RPGBot.UserInterface.Buttons;

public class ProfileButton : ButtonBuilder
{
    public ProfileButton()
    {
        Label = "Profile";
        Style = ButtonStyle.Primary;
        CustomId = "profileButton";
    }
}