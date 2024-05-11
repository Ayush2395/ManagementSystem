using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Application.Common.Models;
using ManagementSystem.Application.Common.Security;
using ManagementSystem.Domain.Constants;
using Microsoft.Extensions.Logging;

namespace ManagementSystem.Application.Users.Commands.UpdateUserId;

[Authorize(Roles = Roles.Administrator)]
public sealed record UpdateUserIdCommand(string UserId, string FirstName, string LastName) : IRequest<Result>;

internal sealed class UpdateUserIdCommandValidator : AbstractValidator<UpdateUserIdCommand>
{
    public UpdateUserIdCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required to update.");
    }
}

internal sealed class UpdateUserIdCommandHandler(IApplicationDbContext context, ILogger<UpdateUserIdCommandHandler> logger) : IRequestHandler<UpdateUserIdCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ILogger<UpdateUserIdCommandHandler> _logger = logger;

    public async Task<Result> Handle(UpdateUserIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.UserProfiles.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);
            if (user is not null)
            {
                user.Id = request.UserId;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure([ex.Message]);
        }
    }
}
