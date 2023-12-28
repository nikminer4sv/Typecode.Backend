using Typecode.Application.ViewModels.TestViewModel;
using Typecode.Domain;

namespace Typecode.Application.Interfaces;

public interface ITestService
{
    Task<Guid> AddAsync(TestViewModel testViewModel);

    Task DeleteAsync(Guid id);

    Task UpdateAsync(Guid id, TestViewModel testViewModel);

    Task<IEnumerable<Test>> GetAllAsync();
}