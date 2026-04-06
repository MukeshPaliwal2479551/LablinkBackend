using LabLinkBackend.Models;

namespace LabLinkBackend.Repositories;

public interface IUserRepository
{
    public Task<User?> GetByEmail(string email);
    public Task<User?> GetByPhone(string phone);
    public Task<User> CreateUser(User user);
    public Task<User?> GetById(int id);
    public Task<User> UpdateUser(User user);
    public Task CreateUserRole(UserRole userRole);
    public Task<User?> GetByIdWithRoles(int id);
    public Task DeleteUserRolesByUserId(int userId);
    public Task AddUserRoles(IEnumerable<UserRole> userRoles);
}
