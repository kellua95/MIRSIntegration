using Microsoft.EntityFrameworkCore;
using MIRS.Domain.Interfaces.Repositories;
using MIRS.Domain.Models;

namespace MIRS.Persistence.Repositories;

public class TestRepo:ITestRepo
{
    private readonly ApplicationContext.ApplicationContext _context;

    public TestRepo(ApplicationContext.ApplicationContext context)
    {
        _context = context;
    }
    public async Task<List<Test>> GetTestsAsync()
    {
        return await _context.Tests.ToListAsync();
    }
}