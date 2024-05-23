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
    public virtual DbSet<Inventory> Inventory { get; set; }
    public virtual DbSet<Quest> Quests { get; set; }
    public virtual DbSet<QuestBoardItem> QuestBoard { get; set; }

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

        modelBuilder.Entity<Inventory>()
            .HasKey(i => new { i.UserId, i.GuildId, i.ItemId });

        //modelBuilder.Entity<Item>()
        //    .HasDiscriminator(i => i.Type);

        modelBuilder.Entity<QuestBoardItem>()
            .HasKey(i => new { i.UserId, i.GuildId, i.QuestId });
    }
}
