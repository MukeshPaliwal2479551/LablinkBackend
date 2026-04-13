using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Repositories;

namespace LabLinkBackend.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository testRepository;
        private readonly IAuditLogService auditLogService;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor;

        public TestService(ITestRepository _testRepository, IAuditLogService _auditLogService, Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor)
        {
            testRepository = _testRepository;
            auditLogService = _auditLogService;
            httpContextAccessor = _httpContextAccessor;
        }
        public async Task Create(TestDto dto)
        {
            var existing = await testRepository.GetByCodeAsync(dto.Code);
            if (existing != null)
            {
                throw new InvalidOperationException("Duplicate Test Code is not allowed");
            }
            var entity = new Test
            {
                Code = dto.Code,
                Name = dto.Name,
                DepartmentId = dto.DepartmentId,
                SpecimenTypeId = dto.SpecimenTypeId,
                ContainerTypeId = dto.ContainerTypeId,
                Units = dto.Units,
                RefRangeJson = dto.RefRangeJson,
                IsActive = dto.IsActive,
                VolumeReq = dto.VolumeReq,
                MaxNormalValue = dto.MaxNormalValue,
                MinNormalValue = dto.MinNormalValue,
                TattargetMinutes = dto.TatTargetMinutes
            };
            await testRepository.Create(entity);

            var auditDto = new AuditDto
            {
                UserId = GetCurrentUserId(),
                Action = "Create Test",
                Resource = $"TestId:{entity.TestId}",
                Metadata = $"Test '{entity.Name}' created."
            };
            await auditLogService.CreateLogAsync(auditDto);
        }

        public async Task Deactivate(int id)
        {
            var test = await testRepository.GetById(id);
            if (test == null)
            {
                throw new KeyNotFoundException("Test not found");
            }

            if (await testRepository.IsTestLinkedToAnyPanelAsync(id))
            {
                throw new InvalidOperationException("Cannot deactivate: Test is linked to a panel.");
            }
            if (await testRepository.IsTestLinkedToAnyOrderAsync(id))
            {
                throw new InvalidOperationException("Cannot deactivate: Test is linked to an order.");
            }

            await testRepository.Deactive(test);

            var auditDto = new AuditDto
            {
                UserId = GetCurrentUserId(),
                Action = "Deactivate Test",
                Resource = $"TestId:{id}",
                Metadata = $"Test '{test.Name}' deactivated."
            };
            await auditLogService.CreateLogAsync(auditDto);
        }

        public async Task<Test?> GetById(int id)
        {
            return await testRepository.GetById(id);
        }

        public async Task<IEnumerable<Test>> GetTests(string? Name, string? Code)
        {
            return await testRepository.GetTests(Name, Code);
        }

        public async Task Update(int id, TestDto dto)
        {
            var test = await testRepository.GetById(id);
            if (test == null) throw new KeyNotFoundException("Test not found");

            test.Code = dto.Code;
            test.Name = dto.Name;
            test.DepartmentId = dto.DepartmentId;
            test.SpecimenTypeId = dto.SpecimenTypeId;
            test.ContainerTypeId = dto.ContainerTypeId;
            test.VolumeReq = dto.VolumeReq;
            test.Units = dto.Units;
            test.MaxNormalValue = dto.MaxNormalValue;
            test.MinNormalValue = dto.MinNormalValue;
            test.TattargetMinutes = dto.TatTargetMinutes;
            test.RefRangeJson = dto.RefRangeJson;
            test.IsActive = dto.IsActive;
            await testRepository.Update(test);

            var auditDto = new AuditDto
            {
                UserId = GetCurrentUserId(),
                Action = "Update Test",
                Resource = $"TestId:{id}",
                Metadata = $"TestId={id} updated."
            };
            await auditLogService.CreateLogAsync(auditDto);
        }
        private int GetCurrentUserId()
        {
            var claimValue = httpContextAccessor.HttpContext?
                .User?
                .FindFirst("userId")?
                .Value;
            return int.TryParse(claimValue, out var userId) ? userId : 0;
        }
    }
}