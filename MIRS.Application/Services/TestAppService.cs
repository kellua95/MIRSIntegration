using MIRS.Application.Interfaces;
using MIRS.Domain.Interfaces.DomainServices;
using MIRS.Domain.Interfaces.Repositories;
using MIRS.Domain.Models;

namespace MIRS.Application.Services;

public class TestAppService:ITestAppService
{
    private readonly ITestRepo _testRepo;
    private readonly ITestManager _testManager;

    public TestAppService(ITestRepo testRepo, ITestManager testManager)
    {
        _testRepo = testRepo;
        _testManager = testManager;
    }
    public Task<List<Test>> GetTestsAsync()
    {
        var newManager = _testManager.AddTest(2);
        return _testRepo.GetTestsAsync();
    }
}