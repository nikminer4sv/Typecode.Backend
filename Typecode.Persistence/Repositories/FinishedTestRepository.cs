using Microsoft.EntityFrameworkCore;
using Typecode.Domain;
using Typecode.Persistence.Interfaces;

namespace Typecode.Persistence.Repositories;

public class FinishedTestRepository : IFinishedTestRepository
{
    private readonly ApplicationDbContext _context;

    public FinishedTestRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> AddAsync(FinishedTest finishedTestModel)
    {
        await _context.FinishedTests.AddAsync(finishedTestModel);
        return finishedTestModel.Id;
    }

    public async Task<IEnumerable<FinishedTest>> GetAllByUserIdAsync(Guid userId)
    {
        return await _context.FinishedTests.Where(x => x.UserId == userId).ToListAsync();
    }
}