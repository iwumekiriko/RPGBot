using Discord;
using Discord.Interactions;
using Microsoft.Extensions.Logging;
using RPGBot.Data;
using RPGBot.Database;
using RPGBot.Utils;
using System.Reflection.Emit;

namespace RPGBot.Modules
{
    public partial class GameModule
    {
        private static readonly DefaultEmbed _mainEmbed = new()
        {
            Title = "Main",
            Description = "test123"
        };

        private static readonly ButtonBuilder[] _mainButtons = new ButtonBuilder[]
        {
            new() { Label = "Shop", Style = ButtonStyle.Primary, CustomId = "shopButton" },
            new() { Label = "Inventory", Style = ButtonStyle.Primary, CustomId = "inventoryButton" },
            new() { Label = "Journey", Style = ButtonStyle.Success, CustomId = "journeyButton" }
        };

        private readonly ComponentBuilder _mainComponents = new();
    }
}
