using Microsoft.EntityFrameworkCore;
using RPGBot.Database.Models;

namespace RPGBot.Database;

public partial class RPGBotEntities : DbContext
{
    public RPGBotEntities(DbContextOptions<RPGBotEntities> options)
        : base(options)
    { }
    public virtual DbSet<Guild> Guilds {  get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Player> Players { get; set; }
    public virtual DbSet<Enemy> Enemies { get; set; }
    public virtual DbSet<InventoryItem> Inventory { get; set; }
    public virtual DbSet<QuestBoardItem> QuestBoard { get; set; }
    public virtual DbSet<ImageCache> ImageCaches { get; set; }
    public virtual DbSet<EquipmentItem> Equipment { get; set; }

    protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)
        //{
        //    optionsBuilder.UseNpqSql();
        //}
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>()
            .HasKey(p => new { p.UserId, p.GuildId });

        modelBuilder.Entity<InventoryItem>()
            .HasKey(i => new { i.UserId, i.GuildId, i.ItemId });

        modelBuilder.Entity<QuestBoardItem>()
            .HasKey(i => new { i.UserId, i.GuildId, i.QuestId });

        modelBuilder.Entity<EquipmentItem>()
            .HasKey(i => new { i.UserId, i.GuildId, i.Slot });
    }
}
