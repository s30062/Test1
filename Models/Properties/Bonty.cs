namespace TavernSystem.Models.Properties;


namespace TavernSystem.Models;

public class Bounty
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int AdventurerId { get; set; }
    public Adventurer Adventurer { get; set; }
}

using Microsoft.EntityFrameworkCore;

namespace TavernSystem.Models;

public class TavernDbContext : DbContext
{
    public TavernDbContext(DbContextOptions<TavernDbContext> options) : base(options) { }

    public DbSet<Adventurer> Adventurers { get; set; }
    public DbSet<Bounty> Bounties { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adventurer>()
            .HasIndex(a => a.PersonDataId)
            .IsUnique();

        modelBuilder.Entity<Bounty>()
            .HasOne(b => b.Adventurer)
            .WithMany(a => a.Bounties)
            .HasForeignKey(b => b.AdventurerId);
    }
}
