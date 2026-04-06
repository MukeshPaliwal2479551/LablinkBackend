using LabLinkBackend.Models;

namespace LabLinkBackend.Repositories;

public interface IUserRepository
{
    Task<bool> Delete(int id);
    Task<List<User>> GetUsersAsync(string? namePart, string? phonePart);
}

