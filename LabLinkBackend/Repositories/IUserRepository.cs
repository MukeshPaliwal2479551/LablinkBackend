using LabLinkBackend.Models;
namespace LabLinkBackend.Repositories
{
    public interface IUserRepository
    { 
        Task<bool> DeleteUser(int id);
        Task<List<User>> GetUsersAsync(string? namePart, string? phonePart);
    }
}