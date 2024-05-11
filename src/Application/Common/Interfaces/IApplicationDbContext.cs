using ManagementSystem.Domain.Entities;

namespace ManagementSystem.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<UserProfile> UserProfiles { get; }

    DbSet<Country> Countries { get; }

    DbSet<State> States { get; }

    DbSet<City> Cities { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
