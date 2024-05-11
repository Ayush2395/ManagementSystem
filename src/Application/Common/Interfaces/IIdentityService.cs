using ManagementSystem.Application.Common.Models;

namespace ManagementSystem.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
    Task<bool> AddToRoleAsync(string uid, string role);

    Task<Result> DeleteUserAsync(string userId);
}
