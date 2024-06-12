using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data.Entities;

namespace ProjectManagement.Data;

public class ProjectContext : IdentityDbContext<User, IdentityRole<string>, string>
{
    public ProjectContext(DbContextOptions<ProjectContext> options)
    : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Assignment> Assignments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Project>()
            .HasMany(p => p.Assignments)
            .WithOne(a => a.Project)
            .HasForeignKey(a => a.ProjectId);

        builder.Entity<Assignment>()
            .HasOne(a => a.AssignedToUser)
            .WithMany(u => u.Assignments)
            .HasForeignKey(a => a.AssignedToUserId)
            .IsRequired(false);

        builder.Entity<Project>()
            .HasMany(p => p.Members)
            .WithMany(u => u.Projects);

        builder.Entity<IdentityRole<string>>()
          .Property(b => b.ConcurrencyStamp)
          .IsETagConcurrency();
        builder.Entity<User>()
            .Property(b => b.ConcurrencyStamp)
            .IsETagConcurrency();
    }
}
