using MIRS.Domain.Models;

namespace MIRS.Domain.Interfaces.DomainServices;

public interface ITestManager
{
    public Test AddTest(int id);
}