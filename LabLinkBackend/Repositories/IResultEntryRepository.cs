using LabLinkBackend.Models;

namespace LabLinkBackend.Repositories;

public interface IResultEntryRepository
{
    Task<ResultEntry> AddAsync(ResultEntry resultEntry);
    Task<ResultEntry?> GetByIdAsync(int resultId);
    Task<List<ResultEntry>> GetByOrderItemIdAsync(int orderItemId);
    Task<ResultEntry> UpdateAsync(ResultEntry resultEntry);
}
