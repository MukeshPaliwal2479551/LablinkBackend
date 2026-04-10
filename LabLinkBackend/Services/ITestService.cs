using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabLinkBackend.DTO;
using LabLinkBackend.Models;

namespace LabLinkBackend.Services
{
    public interface ITestService
    {
        Task Create(CreateTestDto dto);
        Task<IEnumerable<Test>> GetTests(string?Name, string?Code);
        Task<Test?> GetById(int id);
        Task Update(int id,UpdateTestDto dto);
        Task Deactivate(int id);
    }
}