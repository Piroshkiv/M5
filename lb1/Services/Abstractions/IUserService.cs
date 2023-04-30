using System.Threading.Tasks;
using lb1.Dtos;
using lb1.Dtos.Responses;
using lb1.Dtos.Responses.Users;

namespace lb1.Services.Abstractions;

public interface IUserService
{
    Task<UserDto> GetUserById(int id);
    Task<UserResponseCreate> CreateUser(string name, string job);
    Task<BasePageResponse<UserDto>> GetUsersPage(int id);
    Task<UserResponseUpdate> PutUpdateUser(int id, string name, string job);
    Task<UserResponseUpdate> PatchUpdateUser(int id, string name, string job);
    Task DeleteUser(int id);
}