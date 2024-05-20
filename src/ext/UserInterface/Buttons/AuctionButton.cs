using Discord;

namespace RPGBot.UserInterface.Buttons;

public class AuctionButton : ButtonBuilder
{
    public AuctionButton()
    {
        Label = "Auction";
        Style = ButtonStyle.Primary;
        CustomId = "auctionButton";
    }
}
