using System.ComponentModel.DataAnnotations;

namespace RPGBot.Database.Models;
public partial class Enemy
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int? GainedExp { get; set; }
    public string? Description { get; set; }
    public string? Reward { get; set; }
}