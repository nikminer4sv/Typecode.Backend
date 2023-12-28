using Typecode.Application.Interfaces;
using Typecode.Application.ViewModels.TestViewModel;
using Typecode.Domain;
using Typecode.Persistence.Interfaces;

namespace Typecode.Application.Services;

public class TestService : ITestService
{
    private readonly IUnitOfWork _unitOfWork;

    public TestService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Test>> GetAllAsync()
    {
        return await _unitOfWork.Tests.GetAllAsync();
    }
    
    public async Task<Guid> AddAsync(TestViewModel testViewModel)
    {
        var model = new Test
        {
            Id = new Guid(),
            Title = testViewModel.Title,
            Text = testViewModel.Text
        };
        var guid = await _unitOfWork.Tests.AddAsync(model);
        await _unitOfWork.SaveAsync();
        return guid;
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var test = await _unitOfWork.Tests.GetByIdAsync(id);
        if (test != null)
        {
            await _unitOfWork.Tests.DeleteAsync(test);
            await _unitOfWork.SaveAsync();
        }
    }

    public async Task UpdateAsync(Guid id, TestViewModel testViewModel)
    {
        var test = await _unitOfWork.Tests.GetByIdAsync(id);
        if (test != null)
        {
            var model = new Test
            {
                Id = id,
                Title = testViewModel.Title,
                Text = testViewModel.Title
            };
            await _unitOfWork.Tests.UpdateAsync(model);
            await _unitOfWork.SaveAsync();
        }
    }
}