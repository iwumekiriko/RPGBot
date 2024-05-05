using Discord;
using RPGBot.Components;
using RPGBot.Components.Embeds;

namespace RPGBot.Modules
{
    public partial class GameModule
    {
        private static readonly EmbedBuilder _mainWindowEmbed = new MainWindowEmbed();
        private static readonly ComponentBuilder _mainWindowComponents = new MainWindowComponents();
    }
}
