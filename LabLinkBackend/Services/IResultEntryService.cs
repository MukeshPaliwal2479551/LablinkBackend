using LabLinkBackend.DTO;

namespace LabLinkBackend.Services;

public interface IResultEntryService
{
    Task<ResultEntryResponseDto> CreateAsync(ResultEntryCreateDto dto);
    Task<ResultEntryResponseDto> UpdateAsync(int resultId, ResultEntryCreateDto dto);
    Task<ResultEntryResponseDto> GetByIdAsync(int resultId);
    Task<List<ResultEntryResponseDto>> GetByOrderItemIdAsync(int orderItemId);
}
