namespace Typecode.Persistence.Interfaces;

public interface IUnitOfWork : IDisposable
{

    ITestRepository Tests {
        get;
    }
    
    IFinishedTestRepository FinishedTests {
        get;
    }
    Task<int> SaveAsync();
}