using MIRS.Domain.Models;

namespace MIRS.Domain.Interfaces.Repositories;

public interface ITestRepo
{
    public Task<List<Test>> GetTestsAsync();
}