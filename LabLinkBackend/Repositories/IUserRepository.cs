using LabLinkBackend.Models;

namespace LabLinkBackend.Repositories
{
    public interface IUserRepository
    { 
        Task<User?> DeleteUser(int id);
        Task<List<User>> GetUsersAsync(string? namePart, string? phonePart);
    }
}