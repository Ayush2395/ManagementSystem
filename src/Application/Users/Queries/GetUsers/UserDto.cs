using ManagementSystem.Domain.Entities;

namespace ManagementSystem.Application.Users.Queries.GetUsers;
public class UserDto
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserProfile, UserDto>();
        }
    }
}
