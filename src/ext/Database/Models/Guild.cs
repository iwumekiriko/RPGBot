using System.ComponentModel.DataAnnotations;

namespace RPGBot.Database.Models;
public partial class Guild
{
    [Key]
    public ulong Id { get; set; }

    [MaxLength(2)]
    public string? Locale { get; set; }
}
