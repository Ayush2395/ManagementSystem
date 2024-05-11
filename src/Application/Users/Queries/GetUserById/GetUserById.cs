using System.ComponentModel.DataAnnotations;
using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Application.Common.Mappings;
using ManagementSystem.Application.Common.Security;
using ManagementSystem.Application.Users.Queries.GetUsers;
using ManagementSystem.Domain.Constants;
using Microsoft.Extensions.Logging;

namespace ManagementSystem.Application.Users.Queries.GetUserById;

[Authorize(Roles = Roles.Administrator)]
public sealed record GetUserByIdQuery(string UserId) : IRequest<UserDto>;

internal sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is invalid.");
    }
}

internal sealed class GetUserByIdQueryHandler(IApplicationDbContext context, ILogger<GetUserByIdQueryHandler> logger, IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ILogger<GetUserByIdQueryHandler> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        UserDto user = new();
        try
        {
            var list = await _context.UserProfiles
                .ProjectToListAsync<UserDto>(_mapper.ConfigurationProvider);
            if (list != null)
            {
                user = list.FirstOrDefault(x => x.Id == request.UserId) ?? user;
            }
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        return user;
    }
}
