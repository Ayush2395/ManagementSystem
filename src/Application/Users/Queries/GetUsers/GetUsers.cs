using ManagementSystem.Application.Common.Interfaces;
using ManagementSystem.Application.Common.Mappings;
using ManagementSystem.Application.Common.Security;
using ManagementSystem.Domain.Constants;
using Microsoft.Extensions.Logging;

namespace ManagementSystem.Application.Users.Queries.GetUsers;

[Authorize(Roles = Roles.Administrator)]
public record GetUsersQuery : IRequest<List<UserDto>>;

internal class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {
    }
}

internal class GetUsersQueryHandler(IApplicationDbContext context, ILogger<GetUsersQueryHandler> logger, IMapper mapper) : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly ILogger<GetUsersQueryHandler> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        List<UserDto> users = [];
        try
        {
            var lists = await _context.UserProfiles
                .Where(x => x.FirstName != null)
                .OrderByDescending(x => x.Created)
                .ProjectToListAsync<UserDto>(_mapper.ConfigurationProvider);

            users.AddRange(lists);
            return users;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        return users;
    }
}
