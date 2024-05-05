using Microsoft.EntityFrameworkCore;

namespace RPGBot.Database;

public partial class RPGBotEntities : DbContext
{
    public RPGBotEntities(DbContextOptions<RPGBotEntities> options)
        : base(options)
    { }
    public virtual DbSet<Guilds> Guilds {  get; set; }
    public virtual DbSet<Users> Users { get; set; }
    public virtual DbSet<Players> Players { get; set; }
    public virtual DbSet<Enemies> Enemies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)
        //{
        //    optionsBuilder.UseNpqSql();
        //}
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Players>()
            .HasKey(p => new { p.UserId, p.GuildId });
    }
}
