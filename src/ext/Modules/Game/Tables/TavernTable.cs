using Discord.Interactions;
using Microsoft.EntityFrameworkCore;

using RPGBot.UserInterface.Embeds;
using RPGBot.UserInterface;
using RPGBot.Database.Models;
using RPGBot.Modules.Game.Services;
    
namespace RPGBot.Modules.Game;

public class TavernTable(IServiceProvider services) : BaseModule(services)
{
    [ComponentInteraction("auctionButton")]
    public async Task AuctionHandler()
    {

    }

    [ComponentInteraction("shopButton")]
    public async Task ShopHandler()
    {

    }

    [ComponentInteraction("questBoardButton")]
    public async Task QuestBoardHandler()
    {
        var guild = await _database.Guilds.FindAsync(Context.Guild.Id) ?? new Guild { Id = Context.Guild.Id };
        var user = await _database.Users.FindAsync(Context.User.Id) ?? new User { Id = Context.User.Id };
        var player = await _database.Players.FirstOrDefaultAsync(p => p.Guild == guild && p.User == user);
        if (player == null) return;

        var quests = _database.QuestBoard
            .Where(i => i.UserId == Context.User.Id &&
                        i.GuildId == Context.Guild.Id &&
                        !i.IsFinished)
            .Select(i => new { i.Quest, i.IsStarted })
            .ToDictionary(i => i.Quest, i => i.IsStarted);

        await DeferAsync();
        await FollowupAsync(
            embed: new QuestBoardEmbed(quests).Build(),
            components: new QuestBoardComponent(quests).Build(),
            ephemeral: true
        );
    }
}
