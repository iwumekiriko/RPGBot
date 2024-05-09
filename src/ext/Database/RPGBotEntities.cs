using Microsoft.EntityFrameworkCore;

namespace RPGBot.Database;

public partial class RPGBotEntities : DbContext
{
    public RPGBotEntities(DbContextOptions<RPGBotEntities> options)
        : base(options)
    { }
    public virtual DbSet<Guild> Guilds {  get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Player> Players { get; set; }
    public virtual DbSet<Warrior> Warriors { get; set; }
    public virtual DbSet<Hunter> Hunters { get; set; }
    public virtual DbSet<Mage> Mages { get; set; }
    public virtual DbSet<Enemy> Enemies { get; set; }   
    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<Weapon> Weapons { get; set; }
    public virtual DbSet<Accessory> Accessories { get; set; }
    public virtual DbSet<Inventory> Inventory { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
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

        modelBuilder.Entity<Player>()
            .HasDiscriminator(p => p.ClassType);

        modelBuilder.Entity<Item>()
            .HasDiscriminator(i => i.Type);
    }
}
