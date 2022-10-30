using Application.Common.Utilities;
using Domain.Entities;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Repositories.EntityFramework.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var datas = ChangeTracker
                 .Entries<Entity>();

        foreach (var data in datas)
        {
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreateDate = DateTime.UtcNow,
                EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow,
                _ => DateTime.UtcNow
            };
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        HashingHelper.CreatePasswordHash("SdQnv96ZA4$P78G1Ab3ONa$HC!tDVi", out var passwordHash, out var passwordSalt);

        User[] userEntitySeeds = { new("ce1bf1ec-4854-49b0-a7cc-6c8a902e0aa9", "admin", "admin", "admin@admin.com", passwordSalt, passwordHash, true) };
        modelBuilder.Entity<User>().HasData(userEntitySeeds);

        OperationClaim[] operationClaimEntitySeeds = { new("0460d8c9-75a6-4d68-abc3-f249523f3e93", "admin") };
        modelBuilder.Entity<OperationClaim>().HasData(operationClaimEntitySeeds);

        UserOperationClaim[] userOperationClaimEntitySeeds = { new("5565e2ed-b81d-42b6-a26c-0d08f383ce5f", "ce1bf1ec-4854-49b0-a7cc-6c8a902e0aa9", "0460d8c9-75a6-4d68-abc3-f249523f3e93") };
        modelBuilder.Entity<UserOperationClaim>().HasData(userOperationClaimEntitySeeds);
    }
}