using Typecode.Application.ViewModels.FinishedTestViewModel;
using Typecode.Domain;

namespace Typecode.Application.Interfaces;

public interface IFinishedTestService
{
    Task<Guid> AddAsync(FinishedTestViewModel finishedTestViewModel);

    Task<IEnumerable<FinishedTest>> GetAllByUserIdAsync(Guid userId);
}