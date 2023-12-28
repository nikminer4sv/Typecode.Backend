using Typecode.Domain;
using Typecode.Persistence.Interfaces;

namespace Typecode.Persistence.Repositories;

public class TestRepository : GenericRepository<Test>, ITestRepository
{
    public TestRepository(ApplicationDbContext context): base(context) {}
}