using LabLinkBackend.Models;

namespace LabLinkBackend.Data;

public interface IUserRepository
{
    public Task<User?> GetByEmail(string email);
    public Task<User?> GetByPhone(string phone);
    public Task<User> CreateUser(User user);
    public Task<User?> GetById(int id);
    public Task<User> UpdateUser(User user);
}

