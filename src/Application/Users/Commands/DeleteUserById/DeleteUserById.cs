using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Application.Common.Models;
using ManagementSystem.Application.Common.Security;
using ManagementSystem.Domain.Constants;
using Microsoft.Extensions.Logging;

namespace ManagementSystem.Application.Users.Commands.DeleteUserById;

[Authorize(Roles = Roles.Administrator)]
public sealed record DeleteUserByIdCommand(string UserId) : IRequest<Result>;

internal sealed class DeleteUserByIdCommandValidator : AbstractValidator<DeleteUserByIdCommand>
{
    public DeleteUserByIdCommandValidator()
    {
    }
}

internal sealed class DeleteUserByIdCommandHandler(ILogger<DeleteUserByIdCommand> logger, IIdentityService identityService) : IRequestHandler<DeleteUserByIdCommand, Result>
{
    private readonly ILogger<DeleteUserByIdCommand> _logger = logger;
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _identityService.DeleteUserAsync(request.UserId);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure([ex.Message]);
        }
    }
}
