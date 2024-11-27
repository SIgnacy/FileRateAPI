using Domain.Entities.Items;
using Domain.Entities.Members;
using Domain.Entities.Ratings;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class FileRateContext : DbContext
{
    public DbSet<Item> Item { get; set; }
    public DbSet<Keyword> Keyword { get; set; }
    public DbSet<Member> Member { get; set; }
    public DbSet<Rating> Rating { get; set; }

    public FileRateContext() : base() 
    { }
    public FileRateContext(DbContextOptions<FileRateContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ItemConfiguration).Assembly);

}
