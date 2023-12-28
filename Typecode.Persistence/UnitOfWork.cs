using Typecode.Persistence.Interfaces;
using Typecode.Persistence.Repositories;

namespace Typecode.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public ITestRepository Tests { get; }
    public IFinishedTestRepository FinishedTests { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Tests = new TestRepository(_context);
        FinishedTests = new FinishedTestRepository(_context);
    }
    
    public async void Dispose()
    {
        await _context.DisposeAsync();
    }
    
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}