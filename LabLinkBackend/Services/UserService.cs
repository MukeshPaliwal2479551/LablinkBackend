using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Repositories;

namespace LabLinkBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository _repository)
        {
            repository = _repository;
        }
        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                IsActive = user.IsActive
            };
        }
        public async Task<bool> DeleteUser(int id)
        {
            return await repository.DeleteUser(id);
        }
        public async Task<List<UserDto>> GetUsersAsync(string? name, string? phone)
        {
            var users = await repository.GetUsersAsync(name, phone);
            return users.Select(MapToDto).ToList();
        }
    }
}
