using Discord.Interactions;

using RPGBot.UserInterface;
using RPGBot.UserInterface.Embeds;
using RPGBot.Modules.Game.Services;

namespace RPGBot.Modules.Game;

public class QuestBoardTable(IServiceProvider services) : BaseModule(services)
{
    private static int CurrentQuestId { get; set; }

    [ComponentInteraction("questBoardSelectMenu")]
    public async Task QuestShowcase(string[] selections)
    {
        var player = await GetOrCreatePlayerAsync();
        var questId = Int32.Parse(selections.First());
        var questInfo = await _questBoard.GetQuestInfo(
            player, questId
        );
        CurrentQuestId = questId;
        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new QuestShowcaseEmbed(questInfo.Key).Build();
            message.Components = new QuestShowcaseComponent(questInfo.Value.Item2).Build();
        });
    }
    [ComponentInteraction("backToQuestBoardButton")]
    public async Task QuestBoardBack()
    {
        var player = await GetOrCreatePlayerAsync();
        var playerQuests = await _questBoard.GetPlayerQuests(player);
        await Context.Interaction.DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new QuestBoardEmbed(playerQuests).Build();
            message.Components = new QuestBoardComponent(playerQuests).Build();
        });
    }
    [ComponentInteraction("takeQuestButton")]
    public async Task TakeQuest()
    {
        var player = await GetOrCreatePlayerAsync();
        var quest = _questBoard.GetQuest(CurrentQuestId) ??
            throw new InvalidDataException();
        _questBoard.StartQuest(
            player, quest.Id
        );
        await DeferAsync();
        await ModifyOriginalResponseAsync(message =>
        {
            message.Embed = new QuestShowcaseEmbed(quest).Build();
            message.Components = new QuestShowcaseComponent(true).Build();
        });
    }
    [ComponentInteraction("completeQuestButton")]
    public async Task CompleteQuest()
    {
        var player = await GetOrCreatePlayerAsync();            
        if (await _questBoard.AcceptFinishQuest(player, CurrentQuestId))
        {
            var playerQuests = await _questBoard.GetPlayerQuests(player);
            await DeferAsync();
            await ModifyOriginalResponseAsync(message =>
            {
                message.Embed = new QuestBoardEmbed(playerQuests).Build();
                message.Components = new QuestBoardComponent(playerQuests).Build();
            });
        }
        else
        {
            await RespondAsync("You can't complete quest now", ephemeral: true);
        }
    }
}
