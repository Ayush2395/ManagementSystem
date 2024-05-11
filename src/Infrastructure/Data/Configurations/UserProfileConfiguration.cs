using ManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementSystem.Infrastructure.Data.Configurations;
public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(128);
        builder.Property(x => x.CountryId).HasColumnName("Countries");
        builder.Property(x => x.StateId).HasColumnName("States");
        builder.Property(x => x.CityId).HasColumnName("Cities");

        builder.HasOne(x => x.Country)
            .WithMany(x => x.UserProfiles)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.State)
            .WithMany(x => x.UserProfiles)
            .HasForeignKey(x => x.StateId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Cities)
            .WithMany(x => x.UserProfiles)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
