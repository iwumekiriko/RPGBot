using Discord;
using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using RPGBot.UserInterface;
using RPGBot.UserInterface.Embeds;
using RPGBot.Database.Models;
using RPGBot.Modules.Game.Services;


namespace RPGBot.Modules.Game;

public class MainModule(IServiceProvider services) : BaseModule(services)
{

}
