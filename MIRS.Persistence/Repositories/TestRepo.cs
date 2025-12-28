using Microsoft.EntityFrameworkCore;
using MIRS.Domain.Interfaces.Repositories;
using MIRS.Domain.Models;
using MIRS.Persistence.ApplicationDbContext;

namespace MIRS.Persistence.Repositories;

public class TestRepo:ITestRepo
{
    private readonly ApplicationContext _context;

    public TestRepo(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<List<Test>> GetTestsAsync()
    {
        return await _context.Tests.ToListAsync();
    }
}