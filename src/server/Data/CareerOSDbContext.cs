using CareerOS.Server.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CareerOS.Server.Data;

public class CareerOSDbContext(DbContextOptions<CareerOSDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<ProfileEntity> Profiles => Set<ProfileEntity>();
    public DbSet<StrengthEntity> Strengths => Set<StrengthEntity>();
    public DbSet<ExperienceEntryEntity> ExperienceEntries => Set<ExperienceEntryEntity>();
    public DbSet<ProjectEntryEntity> ProjectEntries => Set<ProjectEntryEntity>();
    public DbSet<SkillGroupEntity> SkillGroups => Set<SkillGroupEntity>();
    public DbSet<CertificationEntity> Certifications => Set<CertificationEntity>();
    public DbSet<EducationEntryEntity> EducationEntries => Set<EducationEntryEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // List<string> properties (Roles, Summary, Highlights, Tags, Items) map
        // to native Postgres `text[]` columns by convention on the Npgsql
        // provider — no explicit configuration needed.

        modelBuilder.Entity<StrengthEntity>().HasIndex(s => s.SortOrder);
        modelBuilder.Entity<ExperienceEntryEntity>().HasIndex(e => e.SortOrder);
        modelBuilder.Entity<ProjectEntryEntity>().HasIndex(p => p.SortOrder);
        modelBuilder.Entity<SkillGroupEntity>().HasIndex(s => s.SortOrder);
        modelBuilder.Entity<CertificationEntity>().HasIndex(c => c.SortOrder);
    }
}
