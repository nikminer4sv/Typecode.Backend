using Microsoft.EntityFrameworkCore;
using Typecode.Application.Interfaces;
using Typecode.Application.Services;
using Typecode.Persistence;
using Typecode.Persistence.Interfaces;

namespace Typecode.WebApi.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection collection, string connectionString)
    {
        collection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Typecode.WebApi"));
        });
        collection.AddTransient<IUnitOfWork, UnitOfWork>();

        return collection;
    }
}