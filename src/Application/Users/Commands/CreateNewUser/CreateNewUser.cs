using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Application.Common.Models;
using ManagementSystem.Application.Common.Security;
using ManagementSystem.Domain.Constants;
using Microsoft.Extensions.Logging;

namespace ManagementSystem.Application.Users.Commands.CreateNewUser;

[Authorize(Roles = Roles.Administrator)]
public sealed record CreateNewUserCommand(string FirstName, string LastName) : IRequest<Result>;

internal sealed class CreateNewUserCommandValidator : AbstractValidator<CreateNewUserCommand>
{
    public CreateNewUserCommandValidator()
    {
    }
}

internal sealed class CreateNewUserCommandHandler(IApplicationDbContext context, IIdentityService identityService, ILogger<CreateNewUserCommandHandler> logger) : IRequestHandler<CreateNewUserCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IIdentityService _identityService = identityService;
    private readonly ILogger<CreateNewUserCommandHandler> _logger = logger;

    public async Task<Result> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _identityService.CreateUserAsync($"{request.FirstName.Trim().Split(" ")[0]}@demo.com", "Test@123");
            var role = await _identityService.AddToRoleAsync(user.UserId, Roles.User);
            if (role)
            {
                await _context.UserProfiles.AddAsync(new()
                {
                    Id = user.UserId,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                }, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            return Result.Failure(["An error occured while adding to role."]);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure([ex.Message]);
        }
    }
}
