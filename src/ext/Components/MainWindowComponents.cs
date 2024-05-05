using Discord;
using RPGBot.Components.Buttons;

namespace RPGBot.Components
{
    public class MainWindowComponents : ComponentBuilder
    {
        public MainWindowComponents()
            => WithButton(new InventoryButton())
              .WithButton(new ShopButton())
              .WithButton(new AuctionButton())
              .WithButton(new QuestBoardButton())
              .WithButton(new ArenaButton())
              .WithButton(new JourneyButton());
    }
}
