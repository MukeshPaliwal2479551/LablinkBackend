using LabLinkBackend.DTO;

namespace LabLinkBackend.Services
{
    public interface IUserService
    {
        Task<UserDto?> DeleteUser(int id);
        Task<List<UserDto>> GetUsersAsync(string? name, string? phone);
    }
}