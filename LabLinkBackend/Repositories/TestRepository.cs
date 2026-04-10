using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LabLinkBackend.Models;
using Microsoft.EntityFrameworkCore;
namespace LabLinkBackend.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly LabLinkDbContext context;
        public TestRepository(LabLinkDbContext _context)
        {
            context=_context;
        }
        public async Task Create(Test test)
        {
            context.Tests.Add(test);
            await context.SaveChangesAsync();
        }

        public async Task Deactive(Test test)
        {
            test.IsActive=false;
            context.Tests.Update(test);
            await context.SaveChangesAsync();
        }

        public async Task<Test?> GetByCodeAsync(string Code)
        {
            return await context.Tests.FirstOrDefaultAsync(t=>t.Code==Code);
        }

        public async Task<Test?> GetById(int id)
        {
            return await context.Tests.FirstOrDefaultAsync(t=>t.TestId==id);
        }

        public async Task<IEnumerable<Test>> GetTests(string? Name, string? Code)
        {
            var query=context.Tests.AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                query=query.Where(t=>t.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Code))
            {
                query=query.Where(t=>t.Code.Contains(Code));
            }
            return await query.ToListAsync();
        }

        public async Task Update(Test test)
        {
            context.Tests.Update(test);
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsTestLinkedToAnyPanelAsync(int testId)
        {
            return await context.PanelTests.AnyAsync(pt => pt.TestId == testId && pt.IsActive);
        }

        public async Task<bool> IsTestLinkedToAnyOrderAsync(int testId)
        {
            return await context.OrderItems.AnyAsync(oi => oi.TestId == testId && oi.IsActive);
        }
    }
}