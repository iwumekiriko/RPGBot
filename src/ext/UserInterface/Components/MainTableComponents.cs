using Discord;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface
{
    public class MainTableComponents : ComponentBuilder
    {
        public MainTableComponents()
            => WithButton(new InventoryButton())
              .WithButton(new ShopButton())
              .WithButton(new AuctionButton())
              .WithButton(new QuestBoardButton())
              .WithButton(new ArenaButton())
              .WithButton(new JourneyButton());
    }
}
