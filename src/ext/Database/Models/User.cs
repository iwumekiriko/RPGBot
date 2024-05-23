using System.ComponentModel.DataAnnotations;

namespace RPGBot.Database.Models;

public partial class User
{
    [Key]
    public ulong Id { get; set; }
}