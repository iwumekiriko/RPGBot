using System.ComponentModel.DataAnnotations;

namespace RPGBot.Database.Models;
public partial class Quest
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string ShortDescription { get; set; }
    public required string FullDescription { get; set; }
    public int RequiredLevel { get; set; }
    public int? ItemId { get; set; }
    public int ExpReward { get; set; }
    public int MoneyReward { get; set; }
    public int NeededToComplete { get; set; }
}