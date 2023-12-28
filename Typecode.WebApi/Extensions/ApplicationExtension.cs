using FluentValidation.AspNetCore;
using Typecode.Application.Interfaces;
using Typecode.Application.Services;
using Typecode.Application.ViewModels.RegisterViewModel;

namespace Typecode.WebApi.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterViewModelValidator>());
        collection.AddScoped<ITestService, TestService>();
        collection.AddScoped<IFinishedTestService, FinishedTestService>();
        return collection;
    }
}