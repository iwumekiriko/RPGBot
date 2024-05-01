using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPGBot.Database;
public partial class Guilds
{
    [Key]
    public ulong Id { get; set; }

    [MaxLength(10)]
    public string? Locale { get; set; }
}
public partial class Users
{
    [Key]
    public ulong Id { get; set; }
}
public partial class Players
{
    public ulong UserId { get; set; }
    [ForeignKey("UserId")]
    public Users User { get; set; }

    public ulong GuildId { get; set; }
    [ForeignKey("GuildId")]
    public Guilds Guild { get; set; }
    public string? Class { get; set; }
}
public partial class Enemies
{
    [Key]
    public required int Id { get; set; }

    public string? Name {  get; set; }
    public int? Health { get; set; }
    public int? Attack { get; set; }
    public int? GainedExp { get; set;}
    public string? Description { get; set; }
}