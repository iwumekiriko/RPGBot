using Microsoft.EntityFrameworkCore;

namespace RPGBot.Database;

public partial class RPGBotEntities : DbContext
{
    public RPGBotEntities(DbContextOptions<RPGBotEntities> options)
        : base(options)
    { }
    public DbSet<Guilds> Guilds {  get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Players> Players { get; set; }
    public DbSet<Enemies> Enemies { get; set; }

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
