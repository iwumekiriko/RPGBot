using Discord;

namespace RPGBot.Components.Buttons;

public class AuctionButton : ButtonBuilder
{
    public AuctionButton()
    {
        Label = "Auction";
        Style = ButtonStyle.Primary;
        CustomId = "auctionButton";
    }
}
