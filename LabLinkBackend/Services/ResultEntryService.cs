using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LabLinkBackend.Services;

public class ResultEntryService : IResultEntryService
{
    private readonly IResultEntryRepository _repository;
    private readonly IAuditLogService _auditLogService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LabLinkDbContext _context;

    // Well-known FlagType values stored in the Flags reference table
    private const string FlagNormal  = "Normal";
    private const string FlagAbnormal = "Abnormal";
    private const string FlagPanic   = "Panic";

    public ResultEntryService(
        IResultEntryRepository repository,
        IAuditLogService auditLogService,
        IHttpContextAccessor httpContextAccessor,
        LabLinkDbContext context)
    {
        _repository        = repository;
        _auditLogService   = auditLogService;
        _httpContextAccessor = httpContextAccessor;
        _context           = context;
    }

    public async Task<ResultEntryResponseDto> CreateAsync(ResultEntryCreateDto dto)
    {
        var test = await _context.Tests.FindAsync(dto.TestId)
            ?? throw new InvalidOperationException($"Test with ID {dto.TestId} not found.");

        int flagId = await ResolveFlagIdAsync(dto.Value, test);

        var entry = new ResultEntry
        {
            OrderItemId  = dto.OrderItemId,
            TestId       = dto.TestId,
            Analyte      = dto.Analyte,
            Value        = dto.Value,
            Units        = dto.Units,
            Source       = dto.Source,
            FlagId       = flagId,
            EnteredBy    = GetCurrentUserId(),
            EnteredDate  = DateTime.UtcNow,
            IsActive     = true
        };

        var created = await _repository.AddAsync(entry);

        await _auditLogService.CreateLogAsync(new AuditDto
        {
            UserId   = GetCurrentUserId(),
            Action   = "ResultEntryCreated",
            Resource = $"ResultEntry/{created.ResultId}",
            Metadata = $"OrderItemId={created.OrderItemId}, TestId={created.TestId}, Value={created.Value}"
        });

        return await BuildResponseDtoAsync(created);
    }

    public async Task<ResultEntryResponseDto> UpdateAsync(int resultId, ResultEntryCreateDto dto)
    {
        var entry = await _repository.GetByIdAsync(resultId)
            ?? throw new InvalidOperationException($"ResultEntry with ID {resultId} not found.");

        var test = await _context.Tests.FindAsync(dto.TestId)
            ?? throw new InvalidOperationException($"Test with ID {dto.TestId} not found.");

        int flagId = await ResolveFlagIdAsync(dto.Value, test);

        entry.OrderItemId = dto.OrderItemId;
        entry.TestId      = dto.TestId;
        entry.Analyte     = dto.Analyte;
        entry.Value       = dto.Value;
        entry.Units       = dto.Units;
        entry.Source      = dto.Source;
        entry.FlagId      = flagId;

        var updated = await _repository.UpdateAsync(entry);

        await _auditLogService.CreateLogAsync(new AuditDto
        {
            UserId   = GetCurrentUserId(),
            Action   = "ResultEntryUpdated",
            Resource = $"ResultEntry/{updated.ResultId}",
            Metadata = $"OrderItemId={updated.OrderItemId}, TestId={updated.TestId}, Value={updated.Value}"
        });

        return await BuildResponseDtoAsync(updated);
    }

    public async Task<ResultEntryResponseDto> GetByIdAsync(int resultId)
    {
        var entry = await _repository.GetByIdAsync(resultId)
            ?? throw new InvalidOperationException($"ResultEntry with ID {resultId} not found.");

        return MapToDto(entry);
    }

    public async Task<List<ResultEntryResponseDto>> GetByOrderItemIdAsync(int orderItemId)
    {
        var entries = await _repository.GetByOrderItemIdAsync(orderItemId);
        return entries.Select(MapToDto).ToList();
    }
    /// Determines the correct FlagId by comparing the numeric value against
    /// the test's normal range.
    /// - Within range          → Normal
    /// - Outside range by ≤20% → Abnormal
    /// - Outside range by >20% → Panic
    /// Falls back to Normal if the value is non-numeric.
    private async Task<int> ResolveFlagIdAsync(string? value, Test test)
    {
        string flagType = FlagNormal;

        if (double.TryParse(value, out double numericValue))
        {
            double min   = test.MinNormalValue;
            double max   = test.MaxNormalValue;
            double range = max - min;

            if (numericValue < min || numericValue > max)
            {
                double deviation = numericValue < min
                    ? (min - numericValue)
                    : (numericValue - max);

                double panicThreshold = range > 0 ? range * 0.20 : 1;

                flagType = deviation > panicThreshold ? FlagPanic : FlagAbnormal;
            }
        }

        var flag = await _context.Flags
            .FirstOrDefaultAsync(f => f.FlagType == flagType && f.IsActive);

        if (flag == null)
        {
            // Fallback: grab the first active flag rather than throwing
            flag = await _context.Flags.FirstOrDefaultAsync(f => f.IsActive)
                ?? throw new InvalidOperationException("No active Flag records found. Please seed the Flags table.");
        }

        return flag.FlagId;
    }

    /// Builds a response DTO after a write operation, reloading navigation
    /// properties that may not have been populated by EF after SaveChanges.

    private async Task<ResultEntryResponseDto> BuildResponseDtoAsync(ResultEntry entry)
    {
        // Reload with navigation properties
        var reloaded = await _repository.GetByIdAsync(entry.ResultId);
        return MapToDto(reloaded!);
    }

    private static ResultEntryResponseDto MapToDto(ResultEntry r) => new()
    {
        ResultId          = r.ResultId,
        OrderItemId       = r.OrderItemId,
        TestId            = r.TestId,
        Analyte           = r.Analyte,
        Value             = r.Value,
        Units             = r.Units,
        Source            = r.Source,
        Flag              = r.Flag?.FlagType,
        EnteredByUsername = r.EnteredByNavigation?.Name,
        EnteredDate       = r.EnteredDate
    };

    private int GetCurrentUserId()
    {
        var claimValue = _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;
        return int.TryParse(claimValue, out var userId) ? userId : 0;
    }
}
