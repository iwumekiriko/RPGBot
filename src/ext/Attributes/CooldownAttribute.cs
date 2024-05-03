using Discord;
using Discord.Interactions;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;

namespace RPGBot.Attributes;

public class Cooldown : PreconditionAttribute
{
    TimeSpan CooldownLength { get; set; }
    bool AdminsAreLimited { get; set; }

    readonly ConcurrentDictionary<CooldownInfo, DateTimeOffset> _cooldowns = new();

    /// <summary>
    /// Sets the cooldown for a user to use this command
    /// </summary>
    /// <param name="seconds">Sets the cooldown in seconds.</param>
    /// <param name="adminsAreLimited">Set whether admins should have cooldowns between commands use.</param>
    public Cooldown(int seconds, bool adminsAreLimited = false)
    {
        CooldownLength = TimeSpan.FromSeconds(seconds);
        AdminsAreLimited = adminsAreLimited;
    }

    public struct CooldownInfo
    {
        public ulong UserId { get; }
        public int commandHashCode { get; }

        public CooldownInfo(ulong userId, int interactionHashCode)
        {
            UserId = userId;
            commandHashCode = interactionHashCode;
        }
    }
        
    public override Task<PreconditionResult> CheckRequirementsAsync(IInteractionContext context, ICommandInfo command, IServiceProvider services)
    {
        if (!AdminsAreLimited && context.User is IGuildUser user && user.GuildPermissions.Administrator)
            return Task.FromResult(PreconditionResult.FromSuccess());

        var cooldownData = _cooldowns.FirstOrDefault(c =>
        {
            return c.Key.UserId == context.User.Id &&
            c.Key.commandHashCode == command.GetHashCode();
        });

        if (cooldownData.Key.UserId != 0)
        {
            var endsAt = cooldownData.Value;
            var difference = endsAt.Subtract(DateTimeOffset.UtcNow);

            if (difference.Ticks > 0)
            {
                return Task.FromResult(PreconditionResult.FromError($"You can use this command at <t:{endsAt.ToUnixTimeSeconds()}:T>"));
            }
            var remainingTime = DateTime.UtcNow.Add(difference);
            _cooldowns.TryUpdate(cooldownData.Key, remainingTime, endsAt);
        }
        else
        {
            var cooldownInfo = new CooldownInfo(context.User.Id, command.GetHashCode());
            if (_cooldowns.TryAdd(cooldownInfo, DateTimeOffset.UtcNow.Add(CooldownLength)))
            {
                Console.WriteLine($"Added cooldown for user {context.User.Username} and command /{command.Name}");
            };
            
        }
        return Task.FromResult(PreconditionResult.FromSuccess());
    }   
    
}
