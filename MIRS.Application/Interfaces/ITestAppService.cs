using MIRS.Domain.Models;

namespace MIRS.Application.Interfaces;

public interface ITestAppService
{
    public Task<List<Test>> GetTestsAsync();
}