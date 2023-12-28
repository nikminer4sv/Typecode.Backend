using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Identity.WebApi.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();
        var roleStore = new RoleStore<IdentityRole>(context);

        if (!context.Roles.Any(r => r.Name == "Admin"))
        {
            roleStore.CreateAsync(new IdentityRole("Admin"));
        }
    }
}