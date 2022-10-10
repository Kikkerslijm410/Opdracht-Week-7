using Microsoft.EntityFrameworkCore;
using Planner.Models;

namespace Planner.Data;

public class PlannerContext : DbContext
{
    public PlannerContext (DbContextOptions<PlannerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Attractie>()
            .HasIndex(a => a.Naam)
            .IsUnique();
        builder.Entity<Reservering>()
            .HasCheckConstraint("DagdeelMaximum", "DagDeel <= 35")
            .HasCheckConstraint("DagdeelMinimum", "DagDeel >= 0");
    }

    public DbSet<Reservering> Reservering { get; set; } = default!;
    public DbSet<Gast> Gast { get; set; } = default!;
    public DbSet<Attractie> Attractie { get; set; } = default!;
}