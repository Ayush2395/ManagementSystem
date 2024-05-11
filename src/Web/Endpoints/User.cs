
using ManagementSystem.Application.Common.Models;
using ManagementSystem.Application.Users.Commands.CreateNewUser;
using ManagementSystem.Application.Users.Commands.DeleteUserById;
using ManagementSystem.Application.Users.Commands.UpdateUserId;
using ManagementSystem.Application.Users.Queries.GetUserById;
using ManagementSystem.Application.Users.Queries.GetUsers;

namespace ManagementSystem.Web.Endpoints;

public class User : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetUsers, $"{nameof(GetUsers)}")
            .MapGet(GetUser, "{uid}")
            .MapPost(UpdateUserById, $"{nameof(UpdateUserById)}")
            .MapPost(CreateUser, $"{nameof(CreateUser)}")
            .MapDelete(DeleteUser, $"{nameof(DeleteUser)}/{{uid}}");
    }

    public async Task<List<UserDto>> GetUsers(ISender sender) => await sender.Send(new GetUsersQuery());
    public async Task<UserDto> GetUser(ISender sender, string uid) => await sender.Send(new GetUserByIdQuery(uid));
    public async Task<Result> UpdateUserById(ISender sender, UpdateUserIdCommand command) => await sender.Send(command);
    public async Task<Result> CreateUser(ISender sender, CreateNewUserCommand command) => await sender.Send(command);
    public async Task<Result> DeleteUser(ISender sender, string uid) => await sender.Send(new DeleteUserByIdCommand(uid));
}
