using Typecode.Application.Interfaces;
using Typecode.Application.ViewModels.FinishedTestViewModel;
using Typecode.Domain;
using Typecode.Persistence.Interfaces;

namespace Typecode.Application.Services;

public class FinishedTestService : IFinishedTestService
{
    private readonly IUnitOfWork _unitOfWork;

    public FinishedTestService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> AddAsync(FinishedTestViewModel finishedTestViewModel)
    {
        var model = new FinishedTest
        {
            Id = new Guid(),
            Accuracy = finishedTestViewModel.Accuracy,
            TestId = finishedTestViewModel.TestId,
            UserId = finishedTestViewModel.UserId,
            Time = finishedTestViewModel.Time
        };
        var id = await _unitOfWork.FinishedTests.AddAsync(model);
        await _unitOfWork.SaveAsync();
        return id;
    }

    public async Task<IEnumerable<FinishedTest>> GetAllByUserIdAsync(Guid userId)
    {
        return await _unitOfWork.FinishedTests.GetAllByUserIdAsync(userId);
    }
}