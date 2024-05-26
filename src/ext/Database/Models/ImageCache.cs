using System.ComponentModel.DataAnnotations;

namespace RPGBot.Database.Models;
public partial class ImageCache
{
    [Key]
    public string ImageUrl { get; set; }

    public string ImageName { get; set; }
}
