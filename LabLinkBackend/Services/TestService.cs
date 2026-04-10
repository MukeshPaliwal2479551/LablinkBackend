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
        public async Task Create(CreateTestDto dto)
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
                MethodId = dto.MethodId,
                SpecimenTypeId = dto.SpecimenTypeId,
                ContainerTypeId = dto.ContainerTypeId,
                Units = dto.Units,
                RefRangeJson = dto.RefRange,
                IsActive = dto.IsActive,
                VolumeReq = dto.VolumeReq,
                MaxNormalValue = dto.MaxNormalValue,
                MinNormalValue = dto.MinNormalValue,
                TattargetMinutes = dto.TatTargetMinutes
            };
            await testRepository.Create(entity);

            // Audit log (get userId from claims)
            int userId = 0;
            var user = httpContextAccessor.HttpContext?.User;
            var userIdClaim = user?.FindFirst("userId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var parsedId))
            {
                userId = parsedId;
            }
            var auditDto = new LabLinkBackend.DTO.AuditDto
            {
                UserId = userId,
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

            // Prevent deactivation if test is in use
            if (await testRepository.IsTestLinkedToAnyPanelAsync(id))
            {
                throw new InvalidOperationException("Cannot deactivate: Test is linked to a panel.");
            }
            if (await testRepository.IsTestLinkedToAnyOrderAsync(id))
            {
                throw new InvalidOperationException("Cannot deactivate: Test is linked to an order.");
            }

            await testRepository.Deactive(test);

            // Audit log
            var auditDto = new LabLinkBackend.DTO.AuditDto
            {
                UserId = 0, // Set this to the actual user id if available
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
            return await testRepository.GetTests(Name,Code);
        }

        public async Task Update(int id, UpdateTestDto dto)
        {
            var test = await testRepository.GetById(id);
            if (test == null) throw new KeyNotFoundException("Test not found");

            test.MethodId = dto.MethodId;
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
        }
    }
}