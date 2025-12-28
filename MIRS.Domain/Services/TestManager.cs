using MIRS.Domain.Interfaces.DomainServices;
using MIRS.Domain.Models;

namespace MIRS.Domain.Services;

public class TestManager:ITestManager
{
    public Test AddTest(int id)
    {
        return new Test()
        {
            Id=id
        };
    }
}