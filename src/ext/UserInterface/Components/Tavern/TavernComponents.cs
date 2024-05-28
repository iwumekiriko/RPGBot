using Discord;
using RPGBot.UserInterface.Buttons;

namespace RPGBot.UserInterface;

public class TavernComponent : ComponentBuilder
{
    public TavernComponent()
        => WithButton(new ShopButton())
          .WithButton(new AuctionButton())
          .WithButton(new QuestBoardButton())
          .WithButton(new BackToMainButton());
}
