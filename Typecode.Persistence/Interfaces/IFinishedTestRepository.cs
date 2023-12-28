using Typecode.Domain;

namespace Typecode.Persistence.Interfaces;

public interface IFinishedTestRepository
{
    Task<Guid> AddAsync(FinishedTest finishedTestModel);

    Task<IEnumerable<FinishedTest>> GetAllByUserIdAsync(Guid userId);
}