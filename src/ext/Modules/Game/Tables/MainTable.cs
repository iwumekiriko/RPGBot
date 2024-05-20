using Discord;
using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using RPGBot.Components;
using RPGBot.Components.Embeds;
using RPGBot.Database;
using RPGBot.Modules.Game.Services;


namespace RPGBot.Modules.Game;

public class MainModule(IServiceProvider services) : BaseModule(services)
{

}
