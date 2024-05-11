using ManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ManagementSystem.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public UserProfile UserProfile { get; set; } = null!;
}
