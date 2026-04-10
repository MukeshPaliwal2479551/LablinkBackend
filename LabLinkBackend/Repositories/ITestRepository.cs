using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LabLinkBackend.Models;

namespace LabLinkBackend.Repositories
{
    public interface ITestRepository
    {
        Task Create(Test test);
        Task<Test?> GetByCodeAsync(string Code);
        Task<IEnumerable<Test>> GetTests(string? Name,string? Code);
        Task<Test?> GetById(int id);
        Task Update(Test test);
        Task Deactive(Test test);
        Task<bool> IsTestLinkedToAnyPanelAsync(int testId);
        Task<bool> IsTestLinkedToAnyOrderAsync(int testId);
    }
}